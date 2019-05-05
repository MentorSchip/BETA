using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SequenceUtility
{
    public void Begin(OrderedEvent[] orderedEvents)
    {
        MonoSim.instance.StartCoroutine(ExecuteEventsInOrder(orderedEvents));
    }

    IEnumerator ExecuteEventsInOrder(OrderedEvent[] orderedEvents)
    {
        for (int i = 0; i < orderedEvents.Length; i++)
        {
            orderedEvents[i].Execute();

            if (orderedEvents[i].waitOneFrame)
                yield return 0;
        }
    }

    public void DeactivateAfterFrameCount(GameObject obj, int deactivateOnFrame)
    {
        MonoSim.instance.StartCoroutine(TimedDeactivation(obj, deactivateOnFrame));
    }

    IEnumerator TimedDeactivation(GameObject obj, int deactivateOnFrame)
    {
        int frame = 0;

        while (frame <= deactivateOnFrame)
        {
            if (frame == deactivateOnFrame)
            {
                //Debug.Log("Deactivating " + gameObject.name + " on frame " + frame);
                obj.SetActive(false);
            }
            else
                yield return 0;

            frame++;
        }
    }
}

[Serializable]
public class OrderedEvent
{
    public UnityEvent OnExecute;
    public bool waitOneFrame;
    public float waitForTime;

    public void Execute()
    {
        OnExecute.Invoke();
    }
}
