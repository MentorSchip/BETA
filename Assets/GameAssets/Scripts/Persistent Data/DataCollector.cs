using System;
using UnityEngine;
using UnityEngine.UI;

// Interface between scene and data to be sent to persistent storage
public class DataCollector : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] Text scoreLabel;
    UserData studentData;

    public void SetUser(UserData user)
    {
        this.studentData = user;
    }

    public void AddResponse(string questionId, string question, string answer)
    {
        studentData.AddResponse(questionId, question, answer);
        PersistentDataManager.instance.Save(studentData);
    }

    public void RefreshDisplay(int score, int levelCount)
    {
        scoreText.text = score + " of " + levelCount;
    }
}
