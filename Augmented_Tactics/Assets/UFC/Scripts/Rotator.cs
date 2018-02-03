using System.Collections;
using UnityEngine;

public class Rotator : MonoBehaviour {

	public float x = 0f;
	public float y = 0f;
	public float z = 0f;

	void Update ()
	{
		transform.Rotate (x, y, z);
	}
}
