using UnityEngine;

// Attach a script derived from this base class to event listeners in a scene
public class GameEventListener : MonoBehaviour
{
    public GameEvent Event;

    public void OnEnable()
    { Event.RegisterListener(this); }

    public void OnDisable()
    { Event.UnregisterListener(this); }

    public virtual void OnEventRaised() { }
    public virtual void OnEventRaised(int value) { }
    public virtual void OnEventRaised(Vector2 value) { }
    public virtual void OnEventRaised(Vector3 value) { }

    public virtual void OnEventRaised(UserData value) { }
}
