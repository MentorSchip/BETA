using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UserRole
{
    Student,
    Mentor,
    Champ
}

// Packages all user data for persistent storage
public class UserData
{
    // User data
    public string Name = "";

    public string Role;
    public UserRole GetRole()
    {
        UserRole result;       
        UserRole.TryParse(Role, out result);
        return result; 
    }
    public void SetRole(UserRole role)
    {
        Role = role.ToString();
    }

    // Student data
    public List<ResponseSet> responseData = new List<ResponseSet>();
    public int Score;

    public bool IsQuestionAnswered(int level, string question)
    {
        if (level >= responseData.Count)
            return false;

        return responseData[level].ContainsQuestion(question);
    }

    public void AddResponse(string levelId, string question, string answer)
    {
        var currentLevel = GetLevelById(levelId);

        if (currentLevel == null)
        {
            currentLevel = new ResponseSet(levelId);
            responseData.Add(currentLevel);
        }
      
        currentLevel.AddIfNew(question, answer);
    }

    private ResponseSet GetLevelById(string levelId)
    {
        foreach (var set in responseData)
        {
            if (set.id == levelId)
                return set;
        }
        return null;
    }

    public void ResetProgress()
    {
        responseData.Clear();
        Score = 0;
    }


    // Mentor data
    private List<ScholarshipSet> _scholarshipsCreated;
    public List<ScholarshipSet> scholarshipsCreated 
    { 
        get
        {
            if (_scholarshipsCreated == null)
                _scholarshipsCreated = new List<ScholarshipSet>();
            
            return _scholarshipsCreated;
        }
        set { _scholarshipsCreated = value; }
    }
    public ScholarshipSet currentScholarship;

    public float totalValuePlaced      // ERROR: adding for all users, not resetting(?)
    {
        get
        {
            float sum = 0;

            if (scholarshipsCreated == null || scholarshipsCreated.Count == 0)
                return 0;

            foreach (ScholarshipSet scholarship in scholarshipsCreated)
                sum += scholarship.value;
            
            return sum;
        }
    }

    public void BeginCreatingScholarship()
    {
        //Debug.Log("New scholarship created!");
        currentScholarship = new ScholarshipSet();
        scholarshipsCreated.Add(currentScholarship);
    }

    public void CancelCreatingScholarship()
    {
        if (scholarshipsCreated.Count == 0)
            return;
        if (currentScholarship == null)
            Debug.LogError("Trying to remove a scholarship even though one is not being created");
        
        scholarshipsCreated.Remove(currentScholarship);

        // Current item is the last item in list
        if (scholarshipsCreated.Count == 0)
            currentScholarship = null;
        else
            currentScholarship = scholarshipsCreated[scholarshipsCreated.Count-1];
    }

    public void AddPicture(Sprite sprite)
    {
        currentScholarship.sprite = sprite;
    }

    public void AddScholarshipQuestions(List<string> questions)
    {
        currentScholarship.questions = questions;
    }

    public void AddScholarshipParameters(float value, string duration)
    {
        currentScholarship.value = value;
        currentScholarship.durationDescription = duration;
    }

    public void RemoveScholarshipParameters()
    {
        currentScholarship.value = 0;
        currentScholarship.durationDescription = "";
    }

    public void AddDemoLevel(ScavengerLevel level)
    {
        currentScholarship = new ScholarshipSet(level);
        //Debug.Log("Adding level..." + currentScholarship.gpsLocation + ", " + level.gpsLocation);
        scholarshipsCreated.Add(currentScholarship);
    }
}
