using UnityEngine;

[CreateAssetMenu(menuName = "MentorSchip/Level/Scavenger Level", fileName = "Scavenger Level")]
public class ScavengerLevel : ScriptableObject
{
    public QuestionSet questionSet;
    [HideInInspector] public GameObject target;
    public Sprite hintImage;
    public string hintText;
}