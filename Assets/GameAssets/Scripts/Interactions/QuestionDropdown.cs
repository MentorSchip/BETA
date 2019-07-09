using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionDropdown : MonoBehaviour
{
    [SerializeField] DataCollector dataCollector;
    [SerializeField] ScavengerHunt scavengerHunt;
    [SerializeField] InputField inputText;
    Dropdown dropdown;
    //QuestionSet currentQuestions;
    List<string> currentQuestions;

    string selectedQuestion;
    int selectedIndex = 0;

    void Awake()
    {
        dropdown = GetComponent<Dropdown>();
    }

    // Read values from external source
    //public void PopulateList(QuestionSet questions)
    public void PopulateList(List<string> questions)
    {
        inputText.text = "";
        dropdown.ClearOptions();

        currentQuestions = questions;
        dropdown.AddOptions(questions);
        //dropdown.AddOptions(questions.values);

        // Refresh value of selectedQuestion so data saves correctly
        DropdownIndexChanged(selectedIndex);
    }

    public void DropdownIndexChanged(int index)
    {
        selectedIndex = index;
        selectedQuestion = currentQuestions[index];
        //selectedQuestion = currentQuestions.values[index];
    }

    public void GatherData()
    {
        // Failure conditions
        if (inputText.text == "")
            return;

        if (selectedQuestion == null)
            selectedQuestion = currentQuestions[0];
            //selectedQuestion = currentQuestions.values[0];
            
        dataCollector.AddResponse(scavengerHunt.GetLevelId(), selectedQuestion, inputText.text);
        scavengerHunt.AdvanceLevel(true);
    }
}
