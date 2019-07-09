using UnityEngine;

public class CameraSwapper : MonoBehaviour
{
    Canvas canvas;

    public void SwapCamera(Camera activeCamera)
    {
        if (canvas == null)
            canvas = GetComponent<Canvas>();

        canvas.worldCamera = activeCamera;
    }
}