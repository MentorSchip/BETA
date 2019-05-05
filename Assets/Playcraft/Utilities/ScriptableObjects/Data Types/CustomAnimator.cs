using UnityEngine;

[CreateAssetMenu(menuName = "Playcraft/Utilities/Data Types/Animator")]
public class CustomAnimator : CustomObject
{
    Animator anim;

    public void SetTrigger(string trigger)
    {
        if (anim == null)
            anim = Instance.GetComponent<Animator>();

        Debug.Log("Triggering " + trigger + " animation in " + Instance.name);
        anim.SetTrigger(trigger);
    }
}