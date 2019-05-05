using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DataEventListener : MonoBehaviour
{
    Text display;

    private void Awake()
    {
        display = GetComponent<Text>();
        PersistentDataManager.instance.SaveDataEvent += DisplayNotification;
        display.enabled = false;
    }

    private void DisplayNotification(string message, float duration)
    {
        if (!gameObject.activeSelf)
            return;
        else if (display == null)
        {
            Debug.LogError("Attempting to display notification, but Text component is null");
            return;
        }

        StartCoroutine(TimedDisplay(message, duration));
    }

    IEnumerator TimedDisplay(string message, float duration)
    {
        display.enabled = true;
        display.text = message;
        yield return new WaitForSeconds(duration);
        display.enabled = false;
    }
}
