using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Mapbox.Unity.Utilities;
using Mapbox.Examples;

// [REFACTOR] break related functionality into delegate classes
public class Mentor : MonoBehaviour
{
    [SerializeField] Dropdown questionDropdown;
    public List<string> questionOptions;    // Available in dropdown list
    string selectedQuestion;
    int selectedQuestionIndex;
    [SerializeField] string questionPrompt;
    const int PROMPT = 0;

    public DynamicText dynamicText = new DynamicText();
    [SerializeField] InputField newQuestionInput;
    CustomInputField inputField;
    bool isNew;

    [SerializeField] Dropdown durationDropdown;
    [SerializeField] string durationPrompt;
    public List<string> durationOptions;
    string selectedDuration;
    int selectedDurationIndex;

    [SerializeField] InputField valueInput;
    [SerializeField] Text valueDisplay;
    float totalScholarshipValue { get { return PersistentDataManager.instance.currentUser.totalValuePlaced; } }

    public UnityEvent OnEnterQuestionStage;
    public UnityEvent OnCompleteQuestionSet;
    public UnityEvent OnAddAnother;
    public UnityEvent OnFinalComplete;

    [SerializeField] Text instructions;
    [SerializeField] float displaySaveResultTime = 0.5f;

    UserData userData { get { return PersistentDataManager.instance.currentUser; } }
    public List<string> questionList = new List<string>();


    public void Initialize()
    {
        inputField = newQuestionInput.gameObject.GetComponent<CustomInputField>();
    }

    public void BeginCreatingScholarship()
    {
        userData.BeginCreatingScholarship();
    }

    public void PictureTaken(Sprite sprite)
    {
        userData.AddPicture(sprite);
    }

    public void QuestionDropdownIndexChanged(int index)
    {
        selectedQuestionIndex = index;

        isNew = (index == PROMPT);
        inputField.SetActive(isNew);

        if (!isNew)
            selectedQuestion = questionOptions[index - 1];             

        dynamicText.RefreshText(isNew);
        dynamicText.RefreshStyle(isNew);
    }

    public void DurationDropdownIndexChanged(int index)
    {
        selectedDurationIndex = index;

        isNew = (index == PROMPT);

        if (!isNew)
            selectedDuration = durationOptions[index - 1];  

        //Debug.Log(selectedDuration);                 
    }

    public void EnterQuestionStage()
    {
        //Debug.Log("Entering question stage...");
        OnEnterQuestionStage.Invoke();
        questionList.Clear();
        PopulateQuestionList();
    }

    // Refresh dropdown list
    public void PopulateQuestionList()
    {
        List<string> questions = new List<string>();
        questions.Add(questionPrompt);

        foreach(string questionOption in questionOptions)
            questions.Add(questionOption);

        questionDropdown.ClearOptions();
        questionDropdown.AddOptions(questions);

        QuestionDropdownIndexChanged(0);
    }

    public void EnterValueStage()
    {
        PopulateDurationList();
    }

    private void PopulateDurationList()
    {
        List<string> durations = new List<string>();
        durations.Add(durationPrompt);

        foreach (string durationOption in durationOptions)
            durations.Add(durationOption);

        durationDropdown.ClearOptions();
        durationDropdown.AddOptions(durations);
    }

    public void OnPressAdd()
    {
        //Debug.Log("OnPressAdd method in Mentor class reached");
        if (IsValidInput(newQuestionInput.text))
            RequestAddQuestion(newQuestionInput.text);
        else if (selectedQuestionIndex != 0)
            RequestAddQuestion(questionOptions[selectedQuestionIndex - 1]);
        else
        {
            instructions.text = "No question entered";
            instructions.text += "\n" + questionList.Count.ToString() + " questions saved";
        }
    }

    void RequestAddQuestion(string newQuestion)
    {
        if (questionList.Contains(newQuestion))
        {
            instructions.text = "Duplicate question";
            instructions.text += "\n" + questionList.Count.ToString() + " questions saved";
            return;
        }

        questionList.Add(newQuestion); 
        instructions.text = questionList.Count.ToString() + " questions saved";

        if (questionOptions.Contains(newQuestion))
            questionOptions.Remove(newQuestion);

        PopulateQuestionList();   
    }

    public void CompleteQuestionSet()
    {
        if (questionList == null)
        {
            Debug.LogError("Attempting to save a null list!");
            return;
        }
        else if (questionList.Count == 0)
            return;

        userData.AddScholarshipQuestions(questionList);
        OnCompleteQuestionSet.Invoke();
    }

    public void CompleteScholarship(bool addAnother)
    {
        float moneyValue = GetCurrencyFromString(valueInput.text);
        
        if (moneyValue == 0)
            return;
        else if (selectedDurationIndex == 0)
            return;
        
        //Debug.Log("Scholarship complete!");
        userData.AddScholarshipParameters(moneyValue, selectedDuration);
        PersistentDataManager.instance.Save(userData);

        valueDisplay.text = "$" + totalScholarshipValue.ToString("#.00");

        if (addAnother)
            OnAddAnother.Invoke();
        else
            OnFinalComplete.Invoke();
    }

    bool IsValidInput(string input)
    {
        if (input == null) return false;
        else if (input == "" || 
                input == "Enter text (144 chars)..." ||
                input == ">>" || 
                input == "Create new question...") return false;
        else return true;
    }

    // Parse string and check for invalid input
    float GetCurrencyFromString(string input)
    {
        float value;

        if(!float.TryParse(input, out value))
        {
            value = 0;
            instructions.text = "Invalid input, use numbers only (1234.56)"; 
        }
        else if (value < 0)
        {
            value = 0;
            instructions.text = "Enter positive value";
        }

        return value;
    }

    public void CancelCreatingScholarship()
    {
        userData.CancelCreatingScholarship();
        PersistentDataManager.instance.Save(userData);
    }

    public void RemoveScholarshipQuestions()
    {
        var questionsToRemove = userData.currentScholarship.questions;

        foreach (var question in questionsToRemove)
        {
            questionOptions.Add(question);
        }

        questionsToRemove.Clear();
        PopulateQuestionList();

        instructions.text = questionList.Count.ToString() + " questions saved";
    }

    public void RemoveScholarshipParameters()
    {
        //Debug.Log("Removing, total was: " + totalScholarshipValue);
        userData.RemoveScholarshipParameters();
        //Debug.Log("Removed, total is: " + totalScholarshipValue);
        valueDisplay.text = "$" + totalScholarshipValue.ToString("#.00");
    }
}
