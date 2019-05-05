using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void BlendedProperty(float percent);

// Helper class used for gradual transitions
public class Blend
{
    int transitionResolution = 100;

    // General purpose method for gradually changing any property from one value to another 
    public IEnumerator SmoothChange(BlendedProperty blend, float transitionTime, AnimationCurve curve)
    {
        float tick = transitionTime / transitionResolution;
        float startTime = Time.time;
        float endTime = startTime + transitionTime;
        float percent = 0;
        float curvePercent = 0;

        while (Time.time < endTime)
        {
            percent = (Time.time - startTime) / transitionTime;
            curvePercent = curve.Evaluate(percent);
            blend(curvePercent);
            yield return new WaitForSeconds(tick);
        }
        yield return null;
    }

    // Delegate methods used to transition a specific property from one value to another
    public Color BlendColor(Color begin, Color end, float percent)
    {
        return begin * (1 - percent) + end * percent;
    }

    public float BlendFloat(float begin, float end, float percent)
    {
        return begin * (1 - percent) + end * percent;
    }

    public Vector3 BlendScale(Vector3 begin, Vector3 end, float percent)
    {
        return begin * (1 - percent) + end * percent;
    }
}
