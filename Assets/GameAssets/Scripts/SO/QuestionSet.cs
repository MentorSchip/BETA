using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MentorSchip/Question Set", fileName = "Question Set")]
public class QuestionSet : ScriptableObject
{
    // Set identifier used to check if a level has already been complete
    // TBD: generate GUIDs to guarantee unique values
    public int id; 

    // All of the questions included in a level 
    public List<string> values;
}