using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR;

// Attach this controller to the main camera, or an appropriate
// ancestor thereof, such as the "player" game object.
public class GyroController : MonoBehaviour
{
    [SerializeField] float dragRate = 0.2f;

    Vector3 startCameraRotation;
    float dragYawDegrees;

    void Start()
    {
        Input.gyro.enabled = true;
        startCameraRotation = transform.rotation.eulerAngles;
    }

    void Update()
    {
        if (XRSettings.enabled)
        {
            // Unity takes care of updating camera transform in VR.
            return;
        }

        // android-developers.blogspot.com/2010/09/one-screen-turn-deserves-another.html
        // developer.android.com/guide/topics/sensors/sensors_overview.html#sensors-coords
        //
        //     y                                       x
        //     |  Gyro upright phone                   |  Gyro landscape left phone
        //     |                                       |
        //     |______ x                      y  ______|
        //     /                                       \
        //    /                                         \
        //   z                                           z
        //
        //
        //     y
        //     |  z   Unity
        //     | /
        //     |/_____ x
        //

        // Update `dragYawDegrees` based on user touch.
        CheckDrag();

        //#if UNITY_EDITOR
        //CheckMouseDrag();
        //#endif

        if (Input.gyro.attitude == Quaternion.identity)
            return;

        transform.localRotation = 
          // Allow user to drag left/right to adjust direction they're facing.
          Quaternion.Euler(0f, -dragYawDegrees, 0f) *

          // Neutral position is phone held upright, not flat on a table.
          Quaternion.Euler(90f, 0f, 0f) *

          // Sensor reading, assuming default `Input.compensateSensors == true`.
          Input.gyro.attitude *

          // So image is not upside down.
          Quaternion.Euler(0f, 0f, 180f);

          //Debug.Log(Input.compensateSensors + " " + transform.localRotation);
    }

    void CheckDrag()
    {
        if (Input.touchCount != 1)
        {
            return;
        }

        Touch touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Moved)
        {
            return;
        }

        dragYawDegrees += touch.deltaPosition.x * dragRate;
    }

    
    /* Vector3 currentMousePosition = Vector3.zero;
    Vector3 lastMousePosition = Vector3.zero;
    float mousePointDelta;
    void CheckMouseDrag()
    {
        if (Input.GetMouseButton(0))
        {
            if (lastMousePosition == Vector3.zero)
                lastMousePosition = currentMousePosition;

            currentMousePosition = Input.mousePosition;
            mousePointDelta = lastMousePosition.x - currentMousePosition.x;
            dragYawDegrees = mousePointDelta * dragRate;
            lastMousePosition = currentMousePosition;
        }

        if (Input.GetMouseButtonUp(0))
            lastMousePosition = Vector3.zero;
    }*/
}
