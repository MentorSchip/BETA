using UnityEngine;

public class MissionBottle : MonoBehaviour
{
    UserData userData { get { return PersistentDataManager.instance.currentUser; } }

    public void AcceptMission()
    {
        //Debug.Log(userData.Role + ", " + UserRole.Student.ToString());
        if (userData.Role == UserRole.Student.ToString())
            gameObject.SetActive(true);
    }
}
