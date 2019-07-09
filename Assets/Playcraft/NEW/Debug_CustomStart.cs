using UnityEngine;
using UnityEngine.Events;

// RENAME: UnityHooks -> StartEvent
// NOTE: Setting game objects active/inactive is covered by AutoSceneSetup
public class Debug_CustomStart : MonoBehaviour
{
    enum StartingPage { Default, Map }
    [SerializeField] StartingPage startingPage;

    [SerializeField] UnityEvent DefaultStart;
    [SerializeField] UnityEvent MapStart;

    private void Start()
    {
        switch (startingPage)
        {
            case StartingPage.Default: DefaultStart.Invoke(); break;
            case StartingPage.Map: MapStart.Invoke(); break;
        }
    }
}
