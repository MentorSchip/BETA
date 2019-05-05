using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class DynamicText
{
    public string prompt;
    public Color promptColor;
    public FontStyle promptFontStyle;
    public Color entryColor;
    public FontStyle entryFontStyle;
    public Text text;

    public void RefreshText(InputField input, bool promptSelection)
    {
        input.text = promptSelection ? prompt : "";
    }

    public void RefreshText(bool promptSelection)
    {
        if (promptSelection)
            text.text = prompt;
    }

    public void RefreshStyle(bool promptSelection)
    {
        text.color = promptSelection ? promptColor : entryColor;
        text.fontStyle = promptSelection ? promptFontStyle : entryFontStyle; 
    }
}
