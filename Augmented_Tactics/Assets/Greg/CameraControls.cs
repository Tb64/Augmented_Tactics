using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//add to parent tile of map
public class CameraControls : MonoBehaviour {
    bool AREnabled = false;
    bool flagDistance = false;
    //speed of rotation/translation, could divide into x and y
    float speedRotation = 200;
    float speedZoom = 0.5f;
    float distance;
    public char cam = '0';
    // Update is called once per frame
    private void Update(){

        if(!AREnabled)
            Zoom();
    }
    private void OnMouseDrag(){
        //switch in case of potential additional cameras in future
        switch (cam){
            //pan map across camera
            case '1':
                if (!flagDistance){
                    //Attempt to find a way to have the map stay where it is when panning
                    //Find exact distance between the camera and map
                    distance = (Camera.main.GetComponent<Transform>().position - this.transform.position).magnitude;
                    flagDistance = true;
                }
                Vector3 mousePos = new Vector3(Input.mousePosition.x,Input.mousePosition.y, distance);
                Vector3 mapPos = Camera.main.ScreenToWorldPoint(mousePos);
                transform.position = mapPos;
                break;
            //rotate object in front of camera, disable lines with yRotation to disable tilt
            default:
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
            Camera.main.GetComponent<Transform>().position = Camera.main.GetComponent<Transform>().position - (Camera.main.GetComponent<Transform>().forward * speedZoom);
        }
    }
    public void Clicked(){
        if (!AREnabled){
            if (cam != '1')
                cam = '1';
            else
                cam = '0';
        }
    }
}
