using Assets.Scripts.Beta.NewImplementation;
using LogicSpawn.RPGMaker.Core;
using LogicSpawn.RPGMaker.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogUI : MonoBehaviour
{
    private DialogHandler DialogHandler
    {
        get { return DialogHandler.Instance; }
    }
    private EventSystem EventSystem
    {
        get { return UIHandler.Instance.EventSystem; }
    }
    public DialogModel DialogModel;
    public GameObject DialogPanel;
    public GameObject PlayerResponsePrefab;
    public static DialogUI Instance;

    void Awake()
    {
        Instance = this;
        DialogPanel.SetActive(false);
    }

    void Start()
    {
    }

    void LateUpdate()
    {
        //DialogPanel.SetActive(DialogHandler.DialogNodeChain != null && DialogHandler.NpcResponse != null);
        DialogPanel.SetActive(DialogHandler.Interacting);

    }

    public void CheckForUpdate () {
        if (DialogHandler.NpcResponse == null ) return;       
        DialogModel.NpcName.text = DialogHandler.DialogNpc.GetName();
        DialogModel.NpcText.text = DialogHandler.NpcResponse.DialogText;

        DialogModel.NpcImage.sprite = GeneralMethods.CreateSprite(DialogHandler.DialogNpc.GetImage());

        DialogModel.PlayerResponseHolder.transform.DestroyChildren();

        DialogHandler.CheckResponses();
        var responses = DialogHandler.GetResponses();
        GameObject firstButton = null;
        foreach (var response in responses)
        {
            var go = Instantiate(PlayerResponsePrefab, Vector3.zero, Quaternion.identity) as GameObject;
            go.transform.SetParent(DialogModel.PlayerResponseHolder.transform, false);
            var playerResponseModel = go.GetComponent<PlayerResponseModel>();
            playerResponseModel.Init(response, response.DialogText);
            firstButton = firstButton ?? go;
        }

        EventSystem.SetSelectedGameObject(firstButton);
	}
}
