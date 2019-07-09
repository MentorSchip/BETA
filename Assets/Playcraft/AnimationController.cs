using UnityEngine;

public enum AnimationType { }

public class AnimationController
{
    Animator myAnim;
    AnimationType currentAnimation;

    public AnimationController(Animator myAnim)
    {
        this.myAnim = myAnim;
    }

    public void SetAnimation(AnimationType anim)
    {
        if (currentAnimation == anim)
            return;

        myAnim.SetTrigger(anim.ToString());
        currentAnimation = anim;
    }
}
