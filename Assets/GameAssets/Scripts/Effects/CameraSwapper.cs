using UnityEngine;

public class CameraSwapper : MonoBehaviour
{
    Canvas canvas;

    [SerializeField] Camera liveFeedCamera;
    [SerializeField] Camera gameCamera;

    public void SwapCamera(bool isGameView)
    {
        if (canvas == null)
            canvas = GetComponent<Canvas>();

        //Debug.Log("Swapping camera. To game view = " + isGameView);
        canvas.worldCamera = isGameView ? gameCamera : liveFeedCamera;
    }
}