using UnityEngine;
using UnityEngine.Events;

public class RaycastTrigger : MonoBehaviour
{
    public UnityEvent OnHit;

    public void Hit()
    {
        //Debug.Log(gameObject.name + " hit!");
        OnHit.Invoke();
    }
}
