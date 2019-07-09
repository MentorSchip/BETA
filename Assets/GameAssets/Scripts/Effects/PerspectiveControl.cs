using UnityEngine;

public class PerspectiveControl : MonoBehaviour
{
    // Properties
    [Range(0f, 1f)] [SerializeField] float zoom;
    public float Zoom
    {
        get { return zoom; }
        set
        {
            zoom = value;
            if (zoom > 1) zoom = 1;
            else if (zoom < 0) zoom = 0;
        }
    }

    private float rotation;
    public float Rotation
    {
        get { return rotation; }
        set
        {
            rotation = value;
            transform.parent.eulerAngles = new Vector3(0f, value, 0f);
        }
    }

    // Application Settings
    [SerializeField] Vector3 closeView, farView;
    [SerializeField] float mouseZoomSensitivity;
    [SerializeField] float touchZoomSenitivity;
    [SerializeField] float mouseRotateSensitivity;
    [SerializeField] float touchRotateSensitivity;

    // Cached
    float x, y, z;
    float lastMouseY, mouseYDelta;
    float lastMouseX, mouseXDelta;
    float touchDelta, lastTouchDelta, touchDeltaChange;
    Touch touchZero, touchOne;
    Vector2 touchZeroPriorPosition, touchOnePriorPosition;
    float touchDeltaMagnitude, priorTouchDeltaMagnitude;
    float deltaChangeMagnitude;
    float turnAngle, turnAngleDelta;
    float priorTurn;
    int rotationDirection;

    private void Update()
    {
        #if UNITY_EDITOR
        CheckMouseDrag();
        #endif  
        CheckTwoFingerSpread();

        SetCameraPosition();
    }

    private void CheckMouseDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMouseX = Input.mousePosition.x;
            lastMouseY = Input.mousePosition.y;
        }
        else if (Input.GetMouseButton(0))
        {
            mouseXDelta = Input.mousePosition.x - lastMouseX;
            mouseYDelta = Input.mousePosition.y - lastMouseY;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            mouseXDelta = 0;
            mouseYDelta = 0;
        }
        
        Zoom -= mouseYDelta * mouseZoomSensitivity;
        Rotation += mouseXDelta * mouseRotateSensitivity;
    }

    private void CheckTwoFingerSpread()
    {
        if (Input.touchCount == 2)
        {
            touchZero = Input.GetTouch(0);
            touchOne = Input.GetTouch(1);

            // Detect two finger zoom
            touchZeroPriorPosition = touchZero.position - touchZero.deltaPosition;
            touchOnePriorPosition = touchOne.position - touchOne.deltaPosition;

            touchDeltaMagnitude = (touchZero.position - touchOne.position).magnitude;
            priorTouchDeltaMagnitude = (touchZeroPriorPosition - touchOnePriorPosition).magnitude;

            deltaChangeMagnitude = priorTouchDeltaMagnitude - touchDeltaMagnitude;

            // Detect two-finger rotation
            turnAngle = Angle(touchZero.position, touchOne.position);
            priorTurn = Angle(touchZero.position - touchZero.deltaPosition, touchOne.position - touchOne.deltaPosition);
            turnAngleDelta = Mathf.DeltaAngle(priorTurn, turnAngle);
        }
        else
        {
            deltaChangeMagnitude = 0;
            turnAngleDelta = 0;
        }

        Zoom += deltaChangeMagnitude * touchZoomSenitivity;

        rotationDirection = touchZero.position.y > touchOne.position.y ? 1 : -1;
        Rotation -= turnAngleDelta * rotationDirection * touchRotateSensitivity;
    }

    private void SetCameraPosition()
    {
        y = Blend(closeView.y, farView.y, zoom);
        z = Blend(closeView.z, farView.z, zoom);

        transform.localPosition = new Vector3(0, y, z);
    }

    private float Blend(float min, float max, float percent)
    {
        return min * (1 - percent) + max * percent;
    }

    private float Angle(Vector2 a, Vector2 b)
    {
        var from = b - a;
        var to = new Vector2(1, 0);

        var result = Vector2.Angle(from, to);
        var cross = Vector3.Cross(from, to);

        if (cross.z > 0)
            result -= 360;

        return result;
    } 
}
