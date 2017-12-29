using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLoader : MonoBehaviour {

    static public Ability LoadSkill(string skillID, GameObject gObj)
    {
        skillID = skillID.ToLower();
        switch (skillID)
        {
            case "basicattack":
                Debug.Log("Loading BasicAttack");
                return new BasicAttack(gObj);

            case "heal":
                return new Heal(gObj);

            case "fire":
                return new Fire(gObj);

            //Brawler
            case "combo":
                Debug.Log("Loading Combo");
                return new Combo(gObj);

            case "cyclonekick":
                return new CycloneKick(gObj);

            case "dragonkick":
                return new DragonKick(gObj);

            case "howlingfist":
                return new HowlingFist(gObj);

            case "gutpunch":
                return new GutPunch(gObj);

            case "twinstrike":
                return new TwinStrike(gObj);

            default:
                return null;
        }
    }
}
