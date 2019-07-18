using UnityEngine;
using UnityEngine.UI;

public class MapScholarshipDisplay : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Text requirements;
    [SerializeField] Text time;
    [SerializeField] Text value;

    ScholarshipSet currentScholarship;

    public void ShowUI(ScholarshipSet scholarshipInfo)
    {
        currentScholarship = scholarshipInfo;

        image.sprite = scholarshipInfo.sprite;
        requirements.text = scholarshipInfo.questions.Count.ToString();
        time.text = TimeUtils.SecondsToCountdown(scholarshipInfo.timeRemaining);
        value.text = "$" + scholarshipInfo.value.ToString();        
    }

    public void Tick()
    {
        if (currentScholarship == null)
            return;

        time.text = TimeUtils.SecondsToCountdown(currentScholarship.timeRemaining);
    }
}