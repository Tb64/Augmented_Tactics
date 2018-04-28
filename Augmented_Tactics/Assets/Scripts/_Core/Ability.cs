using System.Collections;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability
{
    public int levelRequired;
    public int range_max;
    public int range_min;
    public float dwell_time;
    public float damage = 10; // temp to test AI
    public float heal = 0;
    public float manaCost = 0;

    public Animator anim;
    public string abilityName;
    public Sprite abilityImage;

    public string abilityDescription;

    protected GameObject gameObject;
    protected Actor actor;
    protected MonoBehaviour monoBehaviour;
    protected bool canTargetTile;
    protected bool canTargetFriendly;
    protected bool canTargetEnemy;
    public bool canHeal, manaRestore;
    protected bool isAOE;
    protected Vector3[] rangeMarkerPos;

    public virtual void Initialize(GameObject obj)
    {
        canTargetTile       = false;
        canTargetFriendly   = true;
        canTargetEnemy      = true;
        isAOE = false;
        canHeal = false;
        manaRestore = false;
        rangeMarkerPos = null;
        gameObject = obj;
        actor = obj.GetComponent<Actor>();
        anim = gameObject.GetComponentInChildren<Animator>();
        monoBehaviour = actor.GetComponent<MonoBehaviour>();
    }

    /// <summary>
    /// Checks if the skill can be used on first click of target.
    /// </summary>
    public virtual void TargetSkill(GameObject target)
    {
        
    }

    public virtual bool UseSkill(GameObject target)
    {
        //parent.GetComponent<Actor>().useAction();

        if (target == null)
            return false;

        if (!canTargetFriendly && target.tag == gameObject.tag)
        {
            return false;
        }

        if (!canTargetEnemy)
        {
            if((gameObject.tag == "Player" && target.tag == "Enemy") || (gameObject.tag == "Enemy" && target.tag == "Player"))
                return false;
        }

        if (!canTargetTile && target.tag == "Tile")
        {
            return false;
        }

        if (SkillInRange(gameObject, target) == false)
        {
            Debug.Log("Out of range. " + abilityName);
            return false;
        }
        if(!actor.UseMana(manaCost))
        {
            Debug.Log("Not enough mana for " + abilityName + " Cost: " + manaCost + " Mana: " + actor.getManaCurrent());
            return false;
        }
        if (!actor.useAction())
        {
            Debug.Log("Not enough actions. " + abilityName);
            return false;
        }
        ActionSkill(target);
        return true;

    }

    public virtual bool UseSkill(GameObject[] targets)
    {
        foreach (GameObject target in targets)
        {
            if (target == null)
                return false;

            if (!canTargetFriendly && target.tag == gameObject.tag)
            {
                return false;
            }

            if (!canTargetEnemy)
            {
                if ((gameObject.tag == "Player" && target.tag == "Enemy") || (gameObject.tag == "Enemy" && target.tag == "Player"))
                    return false;
            }

            if (!canTargetTile && target.tag == "Tile")
            {
                return false;
            }

            if (SkillInRange(gameObject, target) == false)
            {
                Debug.Log("Out of range. " + abilityName);
                return false;
            }
            if (!actor.UseMana(manaCost))
            {
                Debug.Log("Not enough mana. " + abilityName);
                return false;
            }
            if (!actor.useAction())
            {
                Debug.Log("Not enough actions. " + abilityName);
                return false;
            }
        }
        ActionSkill(targets);
        return true;

    }

    public bool CanUseSkill(GameObject target)
    {
        if (target == null)
            return false;

        if (!canTargetFriendly && target.tag == gameObject.tag)
        {
            return false;
        }

        if (!canTargetEnemy)
        {
            if ((gameObject.tag == "Player" && target.tag == "Enemy") || (gameObject.tag == "Enemy" && target.tag == "Player"))
                return false;
        }

        if (!canTargetTile && target.tag == "Tile")
        {
            return false;
        }

        if (SkillInRange(gameObject, target) == false)
        {
            Debug.Log("Out of range. " + abilityName);
            return false;
        }
        if (manaCost != 0)
        {
            if (manaCost > actor.getManaCurrent())
            {
                Debug.Log("Not Enough mana. " + abilityName);
                return false;
            }
        }
        if (actor.getMoves() <= 0)
        {
            Debug.Log("Not enough actions. " + abilityName);
            return false;
        }
        return true;
    }

    public bool CanUseSkill(GameObject[] targets)
    {
        foreach (GameObject target in targets)
        {
            if (target == null)
                return false;

            if (!canTargetFriendly && target.tag == gameObject.tag)
            {
                return false;
            }

            if (!canTargetEnemy)
            {
                if ((gameObject.tag == "Player" && target.tag == "Enemy") || (gameObject.tag == "Enemy" && target.tag == "Player"))
                    return false;
            }

            if (!canTargetTile && target.tag == "Tile")
            {
                return false;
            }

            if (SkillInRange(gameObject, target) == false)
            {
                Debug.Log("Out of range. " + abilityName);
                return false;
            }
            if (manaCost != 0)
            {
                if (manaCost > actor.getManaCurrent())
                {
                    Debug.Log("Not Enough mana. " + abilityName);
                    return false;
                }
            }
            if (actor.getMoves() <= 0)
            {
                Debug.Log("Not enough actions. " + abilityName);
                return false;
            }
        }
        return true;
    }

    public virtual void ActionSkill(GameObject target)
    {

    }

    public virtual void ActionSkill(GameObject[] targets)
    {

    }

    public virtual void InitializeProjectile(GameObject input)
    {
        KFX_Settings settings = input.GetComponent<KFX_Settings>();
        if (settings == null)
        {
            input.AddComponent(typeof(KFX_Settings));
            settings = input.GetComponent<KFX_Settings>();
        }

        settings.explodePos = true;
        settings.explosionDist = gameObject.transform.lossyScale.y * 0.1f;
    }

    public virtual void InitializeProjectile(GameObject input, GameObject target)
    {
        KFX_Settings settings = input.GetComponent<KFX_Settings>();
        if (settings == null)
        {
            input.AddComponent(typeof(KFX_Settings));
            settings = input.GetComponent<KFX_Settings>();
        }

        settings.explodePos = true;
        settings.explosionDist = gameObject.transform.lossyScale.y * 0.1f;
        settings.targetLocation = target.transform.position;
    }

    public virtual void Projectile(GameObject projectileVFX, GameObject target)
    {
        projectileVFX.transform.position = gameObject.transform.position;
        KFX_Settings settings = projectileVFX.GetComponent<KFX_Settings>();
        if (settings == null)
            InitializeProjectile(projectileVFX, target);
        else
            settings.targetLocation = target.transform.position;

        GameObject vfx = GameObject.Instantiate(projectileVFX);

        //vfx.transform.position = gameObject.transform.position;
        vfx.transform.LookAt(target.transform.position);
        GameObject.Destroy(vfx, 5f);

    }

    /// <summary>
    /// Turns on the range marker for this skill
    /// </summary>
    public virtual void EnableRangeMarker()
    {

    }

    /// <summary>
    /// Use this method if you need to check Skill is sucessful (in range or valid target).
    /// </summary>
    /// <param name="target"></param>
    /// <param name="isSuccessful"></param>
    public virtual void UseSkill(GameObject target, out bool isSuccessful)
    {
        isSuccessful =  false;
    }

    public virtual void UseSkillAsync(GameObject target, out bool isSuccessful)
    {
        isSuccessful = false;
    }

    public bool SkillInRange(Vector3 start, Vector3 end)
    {
        start.y = 0f;
        end.y = 0f;
        float distance = Vector3.Distance(start, end);
        //Debug.Log(abilityName + " Skill Range Check: Start:" + start + " End:" + end + " Distance:" + distance);
        return (distance <= (float)range_max && distance >= (float)range_min );
    }

    public bool SkillInRange(GameObject startObj, GameObject endObj)
    {
        Vector3 start = startObj.GetComponent<Actor>().getCoords();
        Vector3 end = new Vector3(0,0,0);
        if (endObj.GetComponent<Actor>() != null)
            end = endObj.GetComponent<Actor>().getCoords();
        else if (endObj.GetComponent<ClickableTile>()!=null)
            end = endObj.GetComponent<ClickableTile>().getCoords();

        return SkillInRange(start, end);
    }

    protected void rotateAtObj(GameObject target)
    {
        Vector3 newDir = Vector3.RotateTowards(gameObject.transform.forward, target.transform.position, 1f, 0f);
        newDir = new Vector3(newDir.x, gameObject.transform.position.y, newDir.z);

        newDir = new Vector3(target.transform.position.x, gameObject.transform.position.y, target.transform.position.z);
        gameObject.transform.LookAt(newDir);
    }

    protected void rotateAtObj(Vector3 pos)
    {
        gameObject.transform.LookAt(pos);
    }

    public static bool DiceRoll(float attackerDex, float targetDex)
    {
        float buff = attackerDex - targetDex;
        //whomever has higher dexterity gets the buff. so if attacker has more dexterity it adds positive and vice versa
        if (Random.Range(1, 20) + buff < Random.Range(1, 20)) //simulated 20 dice roll
            return false;
        return true;
    }

    //////////////////
    //  Set/Get     //
    //////////////////

    public Vector3[] getRangeMakers()
    {
        return this.rangeMarkerPos;
    }

    public bool hasCustomRange()
    {
        return (rangeMarkerPos != null);
    }

    public bool isAOEAttack()
    {
        return this.isAOE;
    }
}