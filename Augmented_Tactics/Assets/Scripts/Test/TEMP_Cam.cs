using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMP_Cam : MonoBehaviour
{

    private Rigidbody body;

    public float speed = 10f;
    public float touchPanSpeed = 0.1f;
    public float angleDelta = 1f;

    public float perspectiveZoomSpeed = 0.00001f;        // The rate of change of the field of view in perspective mode.
    public float orthoZoomSpeed = 0.5f;        // The rate of change of the orthographic size in orthographic mode.

    private Camera camera;

    private Vector2 touchStartPos;
    private float touchStartTime;
    private float startingAngle;

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
        TouchPan();
        TouchRotate();
        TouchPinchZoom();
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
            float prevTouchDeltaMag = (touchZeroPrevPos.normalized - touchOnePrevPos.normalized).magnitude;
            float touchDeltaMag = (touchZero.position.normalized - touchOne.position.normalized).magnitude;

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
                camera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed * Time.deltaTime;

                // Clamp the field of view to make sure it's between 0 and 180.
                camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 10.1f, 79.9f);
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
            if(touchOne.phase == TouchPhase.Began)
                startingAngle = Vector2.SignedAngle(touchZero.position, touchOne.position);

            // Find the position in the previous frame of each touch.
            //Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            //Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            Vector2 startingVector = touchZero.position - touchOne.position;
            Vector2 endingVector = touchZero.deltaPosition - touchOne.deltaPosition;

            startingAngle = Vector2.SignedAngle(touchZero.position - touchZero.deltaPosition, touchOne.position - touchOne.deltaPosition);
            float angleChange = Vector2.SignedAngle(touchZero.position, touchOne.position) - startingAngle;

            body.transform.rotation = Quaternion.Euler(
                body.transform.rotation.eulerAngles.x,
                body.transform.rotation.eulerAngles.y + (angleChange),
                body.transform.rotation.eulerAngles.z
                );
        }
    }

    void TouchPan()
    {
        if(Input.touchCount == 1)
        {
            Touch touch1 = Input.GetTouch(0);
            if (touch1.phase == TouchPhase.Began)
                touchStartTime = Time.time;

            float dist = touch1.deltaPosition.magnitude;
            float touchDuration = Time.time - touchStartTime;

            DebugMobile.Log("dist " + dist + " touchDuration " + touchDuration);
            if(dist >= GameController.touchDistTreshold |touchDuration >= GameController.touchHoldTreshold)
            {
                Vector3 movement = (touch1.deltaPosition.y * transform.forward * Time.deltaTime) + (touch1.deltaPosition.x * transform.right * Time.deltaTime);

                body.velocity = movement.normalized * touchPanSpeed * dist;
                DebugMobile.Log("body.velocity " + body.velocity);
                //body.velocity = touch1.deltaPosition.normalized * speed;
            }
        }
    }
}

