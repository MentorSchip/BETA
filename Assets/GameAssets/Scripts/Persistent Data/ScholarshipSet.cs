using System.Collections.Generic;
using UnityEngine.UI;

// Stores list of a mentor's selected or written questions for a single level
public class ScholarshipSet
{
    public ScholarshipSet()
    {
        questions = new List<string>();
    }

    //public Image image;
    public List<string> questions;
    public float value;
    public string duration;
}
