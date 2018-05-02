using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShot : MonoBehaviour {
    public float speed = 10f;
    public string targetName;
    public Vector3 targetLocation;
    public float explosionDist;

    public GameObject impactVFX;
    private float oldDist;

    // Use this for initialization
    void Start () {
        Rigidbody rbody = GetComponent<Rigidbody>();
        oldDist = float.MaxValue;
        if (rbody == null)
            return;
        GetKFXsettings();
        rbody.velocity = transform.forward * speed; //adding forward velocity
        Destroy(gameObject, 10f);  //destory the arrow after 10f
	}

    // Update is called once per frame
    void Update () {
        float dist = Vector3.Distance(transform.position, targetLocation);
        transform.LookAt(targetLocation);
        //Debug.Log("Arrow target" + targetLocation);

        if(dist < explosionDist || oldDist < dist)
        {
            if (impactVFX != null)
            {
                impactVFX.transform.position = targetLocation;
                impactVFX.transform.LookAt(targetLocation + transform.up * 10f);
                GameObject vfx = Instantiate<GameObject>(impactVFX);
                Destroy(vfx, 2f);
            }
            Destroy(gameObject);
        }
        oldDist = dist;


    }

    private void Impact()
    {
        impactVFX.transform.position = transform.position;
        impactVFX.transform.LookAt(transform.position + transform.up);
        GameObject vfx = Instantiate<GameObject>(impactVFX);
        Destroy(vfx, 2f);
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
