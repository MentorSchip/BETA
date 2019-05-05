using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UserRole
{
    Student,
    Mentor,
    Champ
}

// Packages all student data for persistent storage
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

    public void AddResponse(int levelId, string question, string answer)
    {
        var currentLevel = GetLevelById(levelId);

        if (currentLevel == null)
        {
            currentLevel = new ResponseSet(levelId);
            responseData.Add(currentLevel);
        }
      
        currentLevel.AddIfNew(question, answer);
    }

    private ResponseSet GetLevelById(int levelId)
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
    public List<ScholarshipSet> scholarshipsCreated;
    public ScholarshipSet scholarshipBeingCreated;

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

    // [TBD] Research how to effectively save images to external storage
    public void AddScholarshipImage(Image image)
    {

    }

    public void BeginCreatingScholarship()
    {
        if (scholarshipsCreated == null)
            scholarshipsCreated = new List<ScholarshipSet>();

        scholarshipBeingCreated = new ScholarshipSet();
        //Debug.Log("New scholarship created!");

        scholarshipsCreated.Add(scholarshipBeingCreated);
    }

    public void CancelCreatingScholarship()
    {
        if (scholarshipsCreated.Count == 0)
            return;
        if (scholarshipBeingCreated == null)
            Debug.LogError("Trying to remove a scholarship even though one is not being created");
        
        scholarshipsCreated.Remove(scholarshipBeingCreated);

        // Current item is the last item in list
        scholarshipBeingCreated = scholarshipsCreated[scholarshipsCreated.Count-1];
    }

    public void AddScholarshipQuestions(List<string> questions)
    {
        scholarshipBeingCreated.questions = questions;
    }

    public void AddScholarshipParameters(float value, string duration)
    {
        scholarshipBeingCreated.value = value;
        scholarshipBeingCreated.duration = duration;
    }

    public void RemoveScholarshipParameters()
    {
        scholarshipBeingCreated.value = 0;
        scholarshipBeingCreated.duration = "";
    }
}
