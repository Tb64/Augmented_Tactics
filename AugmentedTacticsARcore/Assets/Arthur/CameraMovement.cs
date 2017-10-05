using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    #region variables
    private float panSpeed = 10f;
    public float panBorderThickness = 2f;   //distance from edge of screen to pan
    public Vector2 panLimit;
    Vector3 position;
    public float minY = 3f;
    public float maxY = 60f;
    public float scrollSpeed = 100f;
    #endregion

    // Update is called once per frame
    void Update () {
        camMovement();
	}

    private void Start()
    {
        minY = 3f;
        maxY = 60f;
        if (panLimit.x == 0 && panLimit.y == 0)
        {
            panLimit.x = 20f;
            panLimit.y = 20f;
        }
    }

    void camMovement()
    {
        position = transform.position;

        //movement works with wasd keys or moving mouse to edge of screen

        //Move camera up
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            position.z += panSpeed * Time.deltaTime;
        }
        //Move camera down
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            position.z -= panSpeed * Time.deltaTime;
        }
        //Move camera right
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            position.x += panSpeed * Time.deltaTime;
        }
        //Move camera left
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            position.x -= panSpeed * Time.deltaTime;
        }

        //gets input from mouse scroll wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        position.y -= scroll * scrollSpeed * 100f * Time.deltaTime;

        //limits how far the camera can move in x,y,z axis
        position.x = Mathf.Clamp(position.x, -panLimit.x, panLimit.x);
        position.y = Mathf.Clamp(position.y, minY, maxY);
        position.z = Mathf.Clamp(position.z, -panLimit.y, panLimit.y);

        //updates position of camera
        transform.position = position;
    }
}
