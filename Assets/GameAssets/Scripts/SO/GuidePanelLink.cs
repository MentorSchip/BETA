using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "MentorSchip/Triggers/Image Text Panel", fileName = "Info Panel")]
public class GuidePanelLink : CustomObject
{
    public int textIndex;
    Text sceneText;

    public int imageIndex;
    Image sceneImage;

    public void SetText(string text)
    {
        if (sceneText == null)
            sceneText = Instance.transform.GetChild(textIndex).GetComponent<Text>();
        
        sceneText.text = text;
    }

    public void SetSprite(Sprite sprite)
    {
        if (sceneImage == null)
            sceneImage = Instance.transform.GetChild(imageIndex).GetComponent<Image>();

        sceneImage.sprite = sprite;
    }
}
