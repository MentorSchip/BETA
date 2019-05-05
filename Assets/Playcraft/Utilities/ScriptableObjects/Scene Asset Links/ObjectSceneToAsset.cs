//using System;
//using System.Collections;
using UnityEngine;
//using UnityEngine.Events;

public class ObjectSceneToAsset : MonoBehaviour
{
    SequenceUtility timer = new SequenceUtility();

    public CustomObject obj;
    [SerializeField] int deactivateOnFrame = -1;

    protected virtual void Awake()
    {
        obj.Instance = gameObject;

        if (deactivateOnFrame >= 0)
            timer.DeactivateAfterFrameCount(gameObject, deactivateOnFrame);
    }

    public virtual void SetInstance(GameObject instance)
    {
        obj.Instance = instance;
    }
}