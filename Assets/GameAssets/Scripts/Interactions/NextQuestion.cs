using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextQuestion : MonoBehaviour
{
    [SerializeField] Text questionText;
    [SerializeField] InputField inputText;
    [SerializeField] ScavengerHunt game;
    [SerializeField] DataCollector dataCollector;

    public void GatherData()
    {
        // Failure conditions
        if (inputText.text == "")
            return;
         
        dataCollector.AddResponse(game.GetLevelId(), questionText.text, inputText.text);
    }
}
