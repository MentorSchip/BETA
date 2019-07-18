using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Utilities;

// Data for mock scholarship to be auto-created for demo/debug purposes
[CreateAssetMenu(menuName = "MentorSchip/Level/Scavenger Level", fileName = "Scavenger Level")]
public class ScavengerLevel : ScriptableObject
{
    public string imageId;
    public Sprite sprite;
    public string hintText = "Find the Image";
    public List<string> questions;

    [Geocode] public string gpsLocation;
    public int duration;
    public float value;
}