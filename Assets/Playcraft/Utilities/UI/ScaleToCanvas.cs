using UnityEngine;

public class ScaleToCanvas : MonoBehaviour
{
    [SerializeField] float referenceHeight = 2960f;

    void Start()
    {
        Vector3 originalObjectScale = transform.localScale;
        transform.localScale = (Screen.height / referenceHeight) * originalObjectScale;
    }
}
