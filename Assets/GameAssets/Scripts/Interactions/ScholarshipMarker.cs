using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;

public class ScholarshipMarker : MonoBehaviour, IMapSpawnable
{
    // Parameters
    [SerializeField] float scale = 100f;
	[SerializeField] float yOffset;

    // Cached dependencies 
    private ScholarshipSet scholarship;
    private MapPage gameMap;
    private AbstractMap mapbox;
    private Vector2d location = new Vector2d();

    string playerLocationGps { get { return PersistentDataManager.instance.userLocation; } }

    private int moveAttempts = 10;

    public GameObject GameObject()
    {
        return gameObject;
    }

    // * Factor out Initialize
    public void SetPosition(ScholarshipSet scholarship, MapPage gameMap, AbstractMap mapbox)
    {
        this.scholarship = scholarship;
        this.gameMap = gameMap;
        this.mapbox = mapbox;

		location = Conversions.StringToLatLon(scholarship.gpsLocation);
		transform.localScale = new Vector3(scale, scale, scale);

        Invoke("DelayMove", .1f);
    }

    private void DelayMove()
    {
        if (moveAttempts <= 0)
        {
            Debug.LogError("Unable to locate map resource needed to move marker");
            return;
        }

        transform.localPosition = mapbox.GeoToWorldPosition(location, false);
        //Debug.Log("Player GPS = " + playerLocationGps);

        if (transform.localPosition == Vector3.zero)
        {
            moveAttempts--;
            Invoke("DelayMove", .1f * (10 - moveAttempts));
        }
        else
        {
            transform.localPosition += Vector3.up * yOffset;
            //Debug.Log("Placing marker at " + transform.localPosition);
            var playerWorldLocation = mapbox.GeoToWorldPosition(Conversions.StringToLatLon(playerLocationGps), false);
            //Debug.Log("Player world location = " + playerWorldLocation);
        }
    }

    public void OnClick()
    {
        //Debug.Log("Marker clicked!");
        gameMap.OnClickScholarshipMarker(scholarship);
    }
}
