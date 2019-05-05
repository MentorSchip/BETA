using UnityEngine;
using Wikitude;

public class FocusTarget : MonoBehaviour
{
    public void SetLocation()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }
}
