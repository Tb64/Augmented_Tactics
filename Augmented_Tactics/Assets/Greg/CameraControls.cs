using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//add to parent tile of map
public class CameraControls : MonoBehaviour {
    bool AREnabled = false;
    bool flagDistance = false;
    //speed of rotation/zoom, could divide into x and y
    float speedRotation = 200;
    float speedTouch = 0.05f;
    float speedZoom = 0.5f;
    //distance between camera and map foruse in translation
    float distance;
    public char cam = '0';
    // Update is called once per frame
    private void Update(){
        Pan();
        if(!AREnabled)
            Zoom();
    }
    private void OnMouseDrag(){
        //switch in case of potential additional cameras in future
        switch (cam){
            //pan map across camera
            default:
                //mouse controls for panning
                if (!flagDistance){
                    //Find exact distance between the camera and map so map stays stationary when grabbed initially
                    distance = (Camera.main.GetComponent<Transform>().position - this.transform.position).magnitude;
                    flagDistance = true;
                }
                Vector3 mousePos = new Vector3(Input.mousePosition.x,Input.mousePosition.y, distance);
                Vector3 mapPos = Camera.main.ScreenToWorldPoint(mousePos);
                transform.position = mapPos;
                break;
            //rotate object in front of camera, disable lines with yRotation to disable tilt
            case '1':
                float xRotation = Input.GetAxis("Mouse X") * speedRotation * Mathf.Deg2Rad;
                float yRotation = Input.GetAxis("Mouse Y") * speedRotation * Mathf.Deg2Rad;
                //Current configuration rotates map along y axis with left/right movement
                //and along camera's x axis
                //x and y relative to camera
                //transform.RotateAround(this.transform.position, Camera.main.transform.up, -xRotation);
                transform.RotateAround(this.transform.position, Camera.main.transform.right, yRotation);
                //x and y relative to object
                transform.Rotate(Vector3.up, -xRotation);
                //transform.Rotate(Vector3.right, yRotation);
                break;
        }
    }
    private void OnMouseUp(){
        //allow distance to be updated again
        flagDistance = false;
    }
    private void Zoom(){
        //zoom with FoV alterations (no clipping but messes with ui)
        /*if (Input.GetAxis("Mouse ScrollWheel") > 0){
            Camera.main.fieldOfView--;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0){
            Camera.main.fieldOfView++;
        }*/
        //zoom by moving camera
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Camera.main.GetComponent<Transform>().position = Camera.main.GetComponent<Transform>().position + (Camera.main.GetComponent<Transform>().forward * speedZoom);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            Camera.main.GetComponent<Transform>().position = Camera.main.transform.position - (Camera.main.GetComponent<Transform>().forward * speedZoom);
        }
    }
    private void Pan(){
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved){
            //movement of finger per update
            Vector3 deltaTouchPos = Input.GetTouch(0).deltaPosition;
            transform.TransformDirection(Camera.main.transform.forward);
            transform.Translate(deltaTouchPos.x * speedTouch, deltaTouchPos.y *speedTouch, 0);
        }
    }
    public void ToggleClicked(){
        if (!AREnabled){
            if (cam != '1')
                cam = '1';
            else
                cam = '0';
        }
    }
    // Moves object according to finger movement on the screen
    /*
    var speed : float = 0.1;
function Update()
    {
        if (Input.touchCount > 0
        Input.GetTouch(0).phase == TouchPhase.Moved) {

            // Get movement of the finger since last frame
            var touchDeltaPosition:Vector2 = Input.GetTouch(0).deltaPosition;

            // Move object across XY plane
            transform.Translate(-touchDeltaPosition.x * speed,
            -touchDeltaPosition.y * speed, 0);
        }
    }
    */
}
