using UnityEngine.Events;

public class UnityEventListener : GameEventListener
{
    public UnityEvent Response;

    public override void OnEventRaised()
    { Response.Invoke(); }
}
