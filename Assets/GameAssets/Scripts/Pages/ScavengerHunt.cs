using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Wikitude;

public class ScavengerHunt : MonoBehaviour, IGame
{
    [SerializeField] Text hintText;
    [SerializeField] Image hintImage;
    [SerializeField] Text question;
    [SerializeField] InputField input;
    int questionIndex;

    CustomMath math = new CustomMath();

    [SerializeField] GameObject[] imageTargets;
    [SerializeField] LocationData levelData;
    ScavengerLevel[] levels 
    { 
        get { return levelData.prebuiltLevels; } 
        set { levelData.prebuiltLevels = value; } 
    }
    int levelIndex;

    DataCollector scoreboard;
    UserData userData { get { return PersistentDataManager.instance.currentUser; } }
    ScholarshipSet currentLevel { get { return userData.currentScholarship; } }
    int saveCompleteOffset = 0;

    [SerializeField] UnityEvent OnFindApplication;
    [SerializeField] UnityEvent OnResumeSearch;
    [SerializeField] UnityEvent OnCompleteHunt;

    public void Initialize()
    {
        saveCompleteOffset = userData.Score;

        levels = math.Shuffle(levels);
        levelIndex = 0;
    }

    public void ActivateCurrentLevel()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            // [TBD] set target in ScholarshipSet...wait until AR system stabilized
            levels[i].target = imageTargets[i];
            imageTargets[i].SetActive(false);
        }

        imageTargets[levelIndex].SetActive(true);
        hintImage.sprite = currentLevel.sprite;
        hintText.text = currentLevel.hintText;

        if (scoreboard == null)
            scoreboard = GetComponent<DataCollector>();
        
        //Debug.Log("Refreshing display: " + levelIndex + ", " + saveCompleteOffset); 
        scoreboard.RefreshDisplay(levelIndex + saveCompleteOffset, levels.Length);

        if(IsLevelComplete(currentLevel))
        {
            //Debug.Log("Level complete, auto-advancing..."); 
            saveCompleteOffset--;
            AdvanceLevel(false);  
        }
    }

    private bool IsLevelComplete(ScholarshipSet level)
    {
        if (userData == null)
        {
            Debug.LogError("Checking if level complete before initializing save data!");
            return false;
        }

        for (int i = 0; i < userData.responseData.Count; i++)
            if (userData.responseData[i].id == level.id)
                return true;

        return false;
    }

    public void OnClickTrackable(GameObject found)
    {
        if (found != currentLevel.target)
            return;

        //Debug.Log("OnClickTrackable, found " + found.name + ", current level target = " + currentLevel.target.name);
        // Hide AR image
        currentLevel.target.SetActive(false);
        
        OnFindApplication.Invoke();
        SetQuestion(0);
    }

    public string GetLevelId()
    {
        return currentLevel.id;
    }

    private void SetQuestion(int index)
    {
        this.questionIndex = index;
        string currentQuestion = currentLevel.questions[questionIndex];
        //string currentQuestion = currentLevel.questionSet.values[questionIndex];

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
            userData.AddResponse(currentLevel.id, question.text, input.text);
   
        questionIndex++;

        //if (questionIndex >= currentLevel.questionSet.values.Count)
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

    public void AdvanceLevel(bool playAnimation)
    {
        // Deactivate the completed level's tracker
        currentLevel.target.SetActive(false);

        // Advance the level or end the series
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
