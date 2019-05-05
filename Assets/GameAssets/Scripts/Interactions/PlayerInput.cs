using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase != TouchPhase.Began)
                return;

            CheckForInteraction(touch.position);
        }
        #if UNITY_EDITOR
        else if (Input.GetMouseButtonDown(0))
            CheckForInteraction(Input.mousePosition);
        #endif
    }

    private void CheckForInteraction(Vector3 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit, 1000f))
            if (hit.collider.gameObject.tag == "Interactable")
                hit.collider.gameObject.GetComponent<ImageTrigger>().OnClick();
    }
}
