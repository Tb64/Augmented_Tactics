using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public GameObject projectileVFX;
    public GameObject impactVFX;

    public Vector3 startPos;
    public Vector3 targetPos;
    public float speed = 1.0F;

    private Actor actor;
    private GameObject projectileObj;
    private float startTime;
    private float journeyLength;
    private const float impactDist = 0.2f;

    // Use this for initialization
    void Start () {
        startTime = Time.time;
        journeyLength = Vector3.Distance(startPos, targetPos);
    }

    private void Update()
    {
        if (projectileObj == null)
            return;


        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        projectileVFX.transform.position = Vector3.Lerp(startPos, targetPos, fracJourney);

        if (Vector3.Distance(projectileVFX.transform.position, targetPos) < 0.2f)
        {
            Destroy(projectileVFX);
            if(impactVFX != null)
                Destroy(Instantiate<GameObject>(impactVFX), 5f);
        }

    }

    public void Initialized(Actor user, GameObject projectile, GameObject impact)//, Vector3 tileStart, Vector3 tileEnd, float speed)
    {
        actor = user;
        impactVFX = impact;
        if (projectile.GetComponent<Collider>() != null)
            projectile.GetComponent<Collider>().enabled = false;
        projectileVFX = projectile;
        projectileObj = null;
    }

    public void Initialized(Actor user, GameObject projectile)//, Vector3 tileStart, Vector3 tileEnd, float speed)
    {
        actor = user;
        impactVFX = null;
        if (projectile.GetComponent<Collider>() != null)
            projectile.GetComponent<Collider>().enabled = false;
        projectileVFX = projectile;
        projectileObj = null;
    }

    public void Shoot(Vector3 tileStart, Vector3 tileEnd, float speed)
    {
        startPos = actor.map.TileCoordToWorldCoord(tileStart);
        targetPos = actor.map.TileCoordToWorldCoord(tileEnd);

        projectileObj = Instantiate<GameObject>(projectileVFX, transform);

        projectileVFX.transform.position = startPos;
        projectileVFX.transform.LookAt(targetPos);
        Destroy(projectileObj, 5f);
    }


}
