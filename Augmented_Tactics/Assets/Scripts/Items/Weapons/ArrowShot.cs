using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShot : MonoBehaviour {
    public float speed = 10f;
    public string targetName;
    public Vector3 targetLocation;
    public float explosionDist;

    GameObject impactVFX;

    // Use this for initialization
    void Start () {
        Rigidbody rbody = GetComponent<Rigidbody>();

        if (rbody == null)
            return;
        GetKFXsettings();
        rbody.velocity = transform.forward * speed; //adding forward velocity
        Destroy(gameObject, 10f);  //destory the arrow after 10f
	}

    // Update is called once per frame
    void Update () {
        float dist = Vector3.Distance(transform.position, targetLocation);

        if(dist < explosionDist)
        {
            if (impactVFX != null)
            {
                impactVFX.transform.position = transform.position;
                GameObject vfx = Instantiate<GameObject>(impactVFX);
                Destroy(vfx, 2f);
            }
            Destroy(gameObject);
        }
		
	}

    private void GetKFXsettings()
    {
        KFX_Settings settings = GetComponent<KFX_Settings>();

        if (settings == null)
            return;

        targetLocation = settings.targetLocation;
        explosionDist = settings.explosionDist;
    }
}
