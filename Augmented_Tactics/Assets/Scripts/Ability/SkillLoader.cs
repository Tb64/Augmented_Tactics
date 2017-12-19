using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLoader : MonoBehaviour {

    static public Ability LoadSkill(int skillID, GameObject gObj)
    {
        switch (skillID)
        {
            case 1:
                return new BasicAttack(gObj);

            case 2:
                return new Heal(gObj);

            case 3:
                return new Fire(gObj);

            case 101:
                return new Combo(gObj);

            case 102:
                return new CycloneKick(gObj);

                

            default:
                return null;
        }
    }
}
