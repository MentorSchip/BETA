using UnityEngine;
using UnityEngine.Events;
using Mapbox.Utils;

public class MapPage : MonoBehaviour
{
    [SerializeField] UnityEvent OnClick;
    [SerializeField] MapScholarshipDisplay display;
    private bool markersLocked;

    public void OnClickScholarshipMarker(ScholarshipSet scholarship)
    {
        if (markersLocked)
            return;

        //Debug.Log(geoLocation);
        OnClick.Invoke();
        display.ShowUI(scholarship);
    }

    public void SetLock(bool isLocked)
    {
        markersLocked = isLocked;
    }

    public void Tick()
    {
        display.Tick();
    }
}
