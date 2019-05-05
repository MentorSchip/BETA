using UnityEngine;
using UnityEngine.UI;

public class CustomInputField : MonoBehaviour
{
    public DynamicText dynamicText = new DynamicText();

    InputField input;
    Image image;

    bool lastChangeFromDropdown;

    void LazyInit()
    {
        if (input == null)
            input = GetComponent<InputField>();

        if (image == null)
            image = GetComponent<Image>();
    }

    public void SetActive(bool isActive)
    {
        //Debug.Log("SetActive method in CustomInputField class reached");
        LazyInit();

        image.enabled = isActive;
        input.interactable = isActive;

        dynamicText.RefreshStyle(true);
        dynamicText.RefreshText(input, isActive);

        lastChangeFromDropdown = true;
    }

    public void OnValueChanged()
    {
        if (lastChangeFromDropdown)
        {
            lastChangeFromDropdown = false;
            return;
        }

        dynamicText.RefreshStyle(false);
    }
}
