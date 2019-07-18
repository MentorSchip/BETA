using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Wikitude;

public class ScavengerHunt : MonoBehaviour
{
    [SerializeField] ImageTrackable trackable;
    [SerializeField] Text hintText;
    [SerializeField] Image hintImage;
    [SerializeField] Text question;
    [SerializeField] InputField input;
    int questionIndex;

    CustomMath math = new CustomMath();

    [SerializeField] LocationData levelData;
    ScavengerLevel[] levels 
    { 
        get { return levelData.prebuiltLevels; } 
        set { levelData.prebuiltLevels = value; } 
    }
    ScavengerLevel currentLevel;
    int levelIndex;

    DataCollector scoreboard;
    UserData userData { get { return PersistentDataManager.instance.currentUser; } }
    int saveCompleteOffset = 0;

    [SerializeField] UnityEvent OnFindApplication;
    [SerializeField] UnityEvent OnResumeSearch;
    [SerializeField] UnityEvent OnCompleteHunt;

    public void Initialize()
    {
        saveCompleteOffset = userData.Score;

        levels = math.Shuffle(levels);
        levelIndex = 0;

        ActivateCurrentLevel();
    }

    public void ActivateCurrentLevel()
    {    
        currentLevel = levels[levelIndex];
        //Debug.Log("new level: " + currentLevel.imageId);
        trackable.TargetPattern = currentLevel.imageId;

        hintImage.sprite = currentLevel.sprite;
        hintText.text = currentLevel.hintText;

        if (scoreboard == null)
            scoreboard = GetComponent<DataCollector>();
        
        //Debug.Log("Refreshing display: " + levelIndex + ", " + saveCompleteOffset); 
        scoreboard.RefreshDisplay(levelIndex + saveCompleteOffset, levels.Length);

        if(IsLevelComplete(currentLevel))
        {
            Debug.Log("Level complete, auto-advancing..."); 
            saveCompleteOffset--;
            AdvanceLevel(false);  
        }
    }

    // Checks current level against persistent data to determine if level has already been completed
    private bool IsLevelComplete(ScavengerLevel level)
    {
        if (userData == null)
        {
            Debug.LogError("Checking if level complete before initializing save data!");
            return false;
        }

        Debug.Log("Searching for prior completion of: " + level.imageId + " in " + userData.responseData.Count + " saved entries.");
        for (int i = 0; i < userData.responseData.Count; i++)
        {
            Debug.Log("Checking saved response: " + userData.responseData[i].id);
            if (userData.responseData[i].id == level.imageId)
                return true;
        }

        return false;
    }

    public void OnClickTrackable()
    {
        //Debug.Log("clicked: " + currentLevel.imageId);
        trackable.TargetPattern = "";  
        OnFindApplication.Invoke();

        // [Redundant?] Reinitialize question dropdown
        SetQuestion(0); 
    }

    public string GetLevelId()
    {
        return currentLevel.imageId;
    }

    // Sets question text based on how many questions have been answered so far
    private void SetQuestion(int index)
    {
        this.questionIndex = index;
        string currentQuestion = currentLevel.questions[questionIndex];

        if (userData.IsQuestionAnswered(levelIndex, currentQuestion))
            NextQuestion(true);
        else
        {
            question.text = currentQuestion;
            input.text = "";
        }
    }

    public void NextQuestion(bool skippedLast)
    {
        if (input.text == "")
            return;

        //Debug.Log("skipped last = " + skippedLast);
        if (!skippedLast)
            userData.AddResponse(currentLevel.imageId, question.text, input.text);
   
        questionIndex++;

        //Debug.Log("NextQuestion...qIndex = " + questionIndex + ", out of: " + currentLevel.questions.Count);
        if (questionIndex >= currentLevel.questions.Count)
        {
            AdvanceLevel(true);
            userData.Score++;
            questionIndex = 0;
        }
        else
            SetQuestion(questionIndex);

        PersistentDataManager.instance.Save(userData);
    }

    // Advance the level or end the series
    public void AdvanceLevel(bool playAnimation)
    {
        levelIndex++;

        if (levelIndex >= levels.Length)
            EndHunt();
        else
            ActivateCurrentLevel();

        if (playAnimation)
            OnResumeSearch.Invoke();
    }

    private void EndHunt()
    {
        OnCompleteHunt.Invoke();
        scoreboard.RefreshDisplay(levelIndex + saveCompleteOffset, levels.Length);
    }
}
