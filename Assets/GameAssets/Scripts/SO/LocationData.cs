using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

// System for auto-generating mock scholarships for debug purposes
[CreateAssetMenu(menuName = "MentorSchip/Map Data/Points of Interest")]
public class LocationData : ScriptableObject
{
    public ScavengerLevel[] prebuiltLevels;
}



    /* public void Tick()
    {
        for (int i = 0; i < scholarships.Count; i++)
            scholarships[i].timeRemaining -= 1;
    }*/