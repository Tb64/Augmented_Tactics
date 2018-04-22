using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMP_Cam : MonoBehaviour
{

    private Rigidbody body;

    public float speed = 10f;
    public float angleDelta = 1f;

    public float perspectiveZoomSpeed = 0.5f;        // The rate of change of the field of view in perspective mode.
    public float orthoZoomSpeed = 0.5f;        // The rate of change of the orthographic size in orthographic mode.

    private Camera camera;


    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody>();
        speed = speed * 100f;
        angleDelta = angleDelta * 50f;
        
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        CameraControls();

    }

    void CameraControls()
    {
        float vert = Input.GetAxis("Vertical");
        float hori = Input.GetAxis("Horizontal");

        Vector3 movement = (vert * transform.forward * Time.deltaTime) + (hori * transform.right * Time.deltaTime);

        body.velocity = movement * speed;

        if (Input.GetKey(KeyCode.Q))
        {
            body.transform.rotation = Quaternion.Euler(
                body.transform.rotation.eulerAngles.x,
                body.transform.rotation.eulerAngles.y + (angleDelta * Time.deltaTime),
                body.transform.rotation.eulerAngles.z
                );
        }
        if (Input.GetKey(KeyCode.E))
        {
            body.transform.rotation = Quaternion.Euler(
                body.transform.rotation.eulerAngles.x,
                body.transform.rotation.eulerAngles.y - (angleDelta * Time.deltaTime),
                body.transform.rotation.eulerAngles.z
                );
        }
    }

    void TouchPinchZoom()
    {
        // If there are two touches on the device...
        if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // If the camera is orthographic...
            if (camera.orthographic)
            {
                // ... change the orthographic size based on the change in distance between the touches.
                camera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

                // Make sure the orthographic size never drops below zero.
                camera.orthographicSize = Mathf.Max(camera.orthographicSize, 0.1f);
            }
            else
            {
                // Otherwise change the field of view based on the change in distance between the touches.
                camera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

                // Clamp the field of view to make sure it's between 0 and 180.
                camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 0.1f, 179.9f);
            }
        }
    }


    void TouchRotate()
    {
        // If there are two touches on the device...
        if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            //Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            //Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            Vector2 startingVector = touchZero.position - touchOne.position;
            Vector2 endingVector = touchZero.deltaPosition - touchOne.deltaPosition;

            float angleChange = Vector2.SignedAngle(startingVector, endingVector);
        }
    }
}

