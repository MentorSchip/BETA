using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mapbox.Unity.Utilities;

// All data relating to a single scholarship
public class ScholarshipSet
{
    public ScholarshipSet()
    {
        this.questions = new List<string>();
        
        Debug_SetDefaults();
    }

    // Create mock scholarship from editor-defined values
    public ScholarshipSet(ScavengerLevel level)
    {
        this.id = level.imageId;
        this.gpsLocation = level.gpsLocation;
        this.sprite = level.sprite;
        this.hintText = level.hintText;
        this.questions = level.questions;
        this.value = level.value;
        this.fullDuration = level.duration;
        this.timeRemaining = level.duration;

        Debug_SetDefaults();
    }

    private void Debug_SetDefaults()
    {
        this.isVisible = true;
        this.hintText = "Find the Image";
    }

    public string id;                           // Unique identifier
    [Geocode] public string gpsLocation;        // GPS location of image trigger in real world
    public bool isVisible;                      // Whether the scholarship is viewable by a student
    public Sprite sprite;                       // Image used for AR trigger and as hint
    public List<string> questions;              // Questions asked in scholarship application
    public float value;                         // $ amount to be awarded on completion
    public string durationDescription;          // Text description of amount of time allowed for scholarship
    public int fullDuration;                    // Amount of time allowed for scholarship on creation
    public int timeRemaining;                   // Time until scholarship expires
    public string hintText;

    public void Tick()
    {
        timeRemaining--;
    }
}
