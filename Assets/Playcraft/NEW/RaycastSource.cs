using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastSource : MonoBehaviour
{
    [SerializeField] float raycastDistance = 100f;

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
        Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.red, 1f);

        if (Physics.Raycast(ray, out hit, raycastDistance))
        {
            var interactable = hit.collider.gameObject.GetComponent<RaycastTrigger>();

            if (interactable != null)
                interactable.Hit();
        }        

        //if (Physics.Raycast(ray, out hit, 1000f))
        //    if (hit.collider.gameObject.tag == "Interactable")
        //        hit.collider.gameObject.GetComponent<ImageTrigger>().OnClick();
    }
}

    /* void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Mouse click detected, casting ray from " + Camera.main);
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.red, 1f);

            if (Physics.Raycast(ray, out hit, raycastDistance))
            {
                var interactable = hit.collider.gameObject.GetComponent<RaycastTrigger>();

                if (interactable != null)
                    interactable.Hit();
            }
        }
    }*/
