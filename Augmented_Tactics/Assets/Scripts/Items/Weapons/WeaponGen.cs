using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGen : MonoBehaviour {

    public Weapons weapon;

    public void LoadData()
    {
        //set weapon data
    }

    public Weapons WeaponGenerate(int level, string characterClass, int rarity)
    {
        // search for weapon of this level and class
        weapon = new Weapons();

        //weapon.name = GetName();
        return weapon;
    }

    public void randomGenDamage()
    {
        Random rnd = new Random();
        int pDmgMin = 0;// = getpDmgMin();
        int pDmgMax = 0;// = getpDmgMax();

        int dmg1 = (int)Random.Range(pDmgMin, pDmgMax);
        int dmg2 = (int)Random.Range(pDmgMin, pDmgMax);

        if(dmg1 < dmg2)
        {
            weapon.physical_dmg_max = dmg2;
            weapon.physical_dmg_min = dmg1;
        }
        else
        {
            weapon.physical_dmg_max = dmg1;
            weapon.physical_dmg_min = dmg2;
        }

        int mDmgMin = 0;// = getmDmgMin();
        int mDmgMax = 0;// = getmDmgMax();

        int mdmg1 = (int)Random.Range(mDmgMin, mDmgMax);
        int mdmg2 = (int)Random.Range(mDmgMin, mDmgMax);

        if (dmg1 < dmg2)
        {
            weapon.magic_dmg_max = dmg2;
            weapon.magic_dmg_min = dmg1;
        }
        else
        {
            weapon.magic_dmg_max = dmg1;
            weapon.magic_dmg_min = dmg2;
        }
    }

    public void randomStatBoost()
    {
        // get value of bonus
        Random rnd = new Random();
        int selected = 0;// rnd.next(0, 7);

        switch (selected)
        {
            case 0:     //str
                        //get value
                //int bonusMax = something;
                //this.weapon.str_bonus = rnd.next(0, bonusMax);
                break;

            case 1:     //dex
                break;

            case 2:     //con`
                break;

            case 3:     //wis
                break;

            case 4:     //int
                break;

            case 5:
                break;

            case 6:
                break;

            case 7:
                break;

            default:
                break;
        }

    }
}
