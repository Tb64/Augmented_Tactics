using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARonlyone : MonoBehaviour {

    private void Awake()
    {
        if (GameObject.Find(name) != gameObject)
            Destroy(gameObject);
    }
}
