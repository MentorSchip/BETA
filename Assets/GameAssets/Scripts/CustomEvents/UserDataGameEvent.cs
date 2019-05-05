using UnityEngine;

[CreateAssetMenu(fileName = "User Selected Event", menuName = "MentorSchip/User Selected Event")]
public class UserDataGameEvent : GameEvent
{
    public void Raise(UserData value)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised(value);
    }
}