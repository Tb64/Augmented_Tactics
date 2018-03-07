using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTriggers : MonoBehaviour {

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Hit()
    {
        
    }

    public void Shoot()
    {

    }

    public void FootR()
    {
        FindObjectOfType<SFXController>().Play("rightfoot");
    }

    public void FootL()
    {
        FindObjectOfType<SFXController>().Play("leftfoot");
    }

    public void Land()
    {

    }

    public void WeaponSwitch()
    {

    }
}
