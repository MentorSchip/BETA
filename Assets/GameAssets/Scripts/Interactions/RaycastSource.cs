using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastSource : MonoBehaviour
{
    [SerializeField] float raycastDistance = 100f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Mouse click detected");
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
    }
}
