using UnityEngine;
using UnityEngine.UI;

public class VisualEffect : ScriptableObject
{
    public float duration;
    public AnimationCurve curve;

    public virtual void BeginTransition (Image fadeImage) { }
}
