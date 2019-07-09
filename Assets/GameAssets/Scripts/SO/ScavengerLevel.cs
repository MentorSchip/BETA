using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Utilities;

// Mock scholarship to be auto-created for demo/debug purposes
[CreateAssetMenu(menuName = "MentorSchip/Level/Scavenger Level", fileName = "Scavenger Level")]
public class ScavengerLevel : ScriptableObject
{
    //public QuestionSet questionSet;
    [HideInInspector] public GameObject target;
    public Sprite sprite;
    public List<string> questions;

    [Geocode] public string gpsLocation;
    public int duration;
    public float value;
}