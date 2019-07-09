using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MentorCamera : MonoBehaviour
{
    // Scene references
    [SerializeField] Image guideImage;
    [SerializeField] GameObject aimPanel;

    // Control parameters
    [SerializeField] float heightCrop = 2;
    [SerializeField] float heightShiftPercent = 0;
    
    // Cached variables
    Texture2D texture;
    Rect rectangle;
    float captureHeight;
    float heightShift;
    int verticalLocation;
    Sprite sprite;

    public UnityEvent OnPictureTaken;
    public SpriteEvent BroadcastSprite;

    public void SnapshotButtonPressed()
    {
        StartCoroutine(TakePicture());
    }

    IEnumerator TakePicture()
    {
        // Error prevention
        aimPanel.SetActive(false);
        yield return new WaitForEndOfFrame();

        // Take screenshot
        texture = ScreenCapture.CaptureScreenshotAsTexture();

        // Calculate modified screenshot dimensions
        captureHeight = Screen.height/heightCrop;
        heightShift = Screen.height * heightShiftPercent;
        verticalLocation = (int)(captureHeight/2 + heightShift);
        rectangle = new Rect(0, verticalLocation, Screen.width, captureHeight);

        // Modify screenshot to cover portion of screen
        texture.ReadPixels(rectangle, 0, verticalLocation, false);
        texture.Apply();

        // Update display image
        sprite = Sprite.Create(texture, rectangle, Vector2.zero);
        guideImage.sprite = sprite;

        yield return new WaitForEndOfFrame();
        OnPictureTaken.Invoke();
        BroadcastSprite.Invoke(sprite);
    }
}
