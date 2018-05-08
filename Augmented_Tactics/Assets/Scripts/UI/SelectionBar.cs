using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionBar : MonoBehaviour {

    public Image selectedPortait;
    public Text selectedHPText;
    public Text selectedMPText;
    public Slider selectedHPSlider;
    public Slider selectedMPSlider;

    public Image abilityIcon;

    public Image targetPortait;
    public Text targetHPText;
    public Text targetMPText;
    public Slider targetHPSlider;
    public Slider targetMPSlider;

    private GameObject selectedObj;
    private GameObject targetObj;
    private Sprite noAbility;

    private Actor selected;

    // Use this for initialization
    void Start () {
        selectedObj = selectedPortait.transform.parent.gameObject;
        targetObj = targetPortait.transform.parent.gameObject;
        noAbility = abilityIcon.sprite;
    }
	
	// Update is called once per frame
	void Update () {
        UpdateSelected();
        UpdateTarget();

    }

    private void UpdateSelected()
    {
        selected = GameController.getSelected();

        if (selected == null)
            return;

        if(selected.icon != null)
            selectedPortait.sprite = selected.icon;

        selectedHPText.text = (int)selected.GetHealthCurrent() + "/" + (int)selected.GetHeathMax();
        selectedHPSlider.value = selected.GetHealthPercent();

        selectedMPText.text = (int)selected.getManaCurrent() + "/" + (int)selected.getMaxMana();
        selectedMPSlider.value = selected.GetManaPercent();

        UpdateAbilityIcon(selected);
    }

    private void UpdateTarget()
    {
        GameObject target = GameController.getTargeted();

        if (target == null)
        {
            targetObj.SetActive(false);
            return;
        }
        else if (selected.gameObject == target)
            return;
        else if(target.tag == "Enemy" || target.tag == "Player")
        {
            targetObj.SetActive(true);
            Actor targetActor = target.GetComponent<Actor>();
            targetHPText.text = (int)targetActor.GetHealthCurrent() + "/" + (int)targetActor.GetHeathMax();
            targetHPSlider.value = targetActor.GetHealthPercent();

            targetMPText.text = (int)targetActor.getManaCurrent() + "/" + (int)targetActor.getMaxMana();
            targetMPSlider.value = targetActor.GetManaPercent();
        }
        else
        {
            targetObj.SetActive(false);
        }
    }

    private void UpdateAbilityIcon(Actor selected)
    {
        if (selected == null || GameController.getMode() != GameController.MODE_SELECT_TARGET)
        {
            abilityIcon.sprite = noAbility;
            return;
        }

        abilityIcon.sprite = selected.abilitySet[GameController.getCurrentAbility()].abilityImage;
    }
}
