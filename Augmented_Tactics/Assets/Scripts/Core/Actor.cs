using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour {

    protected float health_current;
    protected float health_max;
    protected float move_speed;
    protected float armor_class;

    //stats
    protected int strength;         //measuring physical power
    protected int dexterity;        //measuring agility
    protected int constitution;     //measuring endurance
    protected int intelligence;     //measuring reasoning and memory (Magic Damage)
    protected int wisdom;           //measuring perception and insight (Resistance/Healing)
    protected int charisma;         //measuring force of personality (Buffs and Debuffs)

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// The method to damage an Actor
    /// </summary>
    /// <param name="damage">Damage the Actor will take as a float</param>
    public void TakeDamage(float damage)
    {

    }
}
