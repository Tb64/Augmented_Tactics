using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTargetter : MonoBehaviour
{

    public string TargetScene;

    public void Trigger()
    {
        Application.LoadLevel(TargetScene);
    }
}
