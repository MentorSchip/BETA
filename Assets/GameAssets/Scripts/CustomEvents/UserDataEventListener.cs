public class UserDataEventListener : GameEventListener
{
    public UserDataEvent UserResponse;

    public override void OnEventRaised(UserData value)
    { UserResponse.Invoke(value); }
}
