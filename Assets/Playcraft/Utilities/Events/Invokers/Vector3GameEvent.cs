using UnityEngine;

[CreateAssetMenu(fileName = "Game Event", menuName = "Playcraft/Utilities/Events/Vector3")]
public class Vector3GameEvent : GameEvent
{
    public void Raise(Vector3 value)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised(value);
    }
}

