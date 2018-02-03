using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLoader : MonoBehaviour {

    /// <summary>
    /// Used for loading skill in to actor.  Note that to add new skills the text input is converted to lowercase, so switch only works with lowercase.
    /// </summary>
    /// <param name="skillID">String of the skill to be loaded</param>
    /// <param name="gObj">The gamobject that the skill is being added to</param>
    /// <returns></returns>
    static public Ability LoadSkill(string skillID, GameObject gObj)
    {
        skillID = skillID.ToLower();
        switch (skillID)
        {
            // ALL CASES MUST BE LOWER CASE!!!!


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

            case "counter":
                return new Counter(gObj);

            //Knight

            case "eviscerate":
                return new Eviscerate(gObj);

            case "disintegrate":
                return new Disintegrate(gObj);

            case "lifeleech":
                return new LifeLeech(gObj);

            default:
                return null;
        }
    }
}
