using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Playcraft/Effects/Scene Fade", fileName = "Scene Fade")]
public class ColorFade : VisualEffect
{
    public Color beforeColor;
    public Color afterColor;

    Blend blend = new Blend();
    IEnumerator sceneFade;
    Image fadeImage;

    public override void BeginTransition(Image fadeImage)
    {
        this.fadeImage = fadeImage;
        
        if (sceneFade != null)
            MonoSim.instance.StopCoroutine(sceneFade);

        sceneFade = blend.SmoothChange(StepFade, duration, curve);
        MonoSim.instance.StartCoroutine(sceneFade);
    }

    private void StepFade(float percent)
    {        
        fadeImage.color = blend.BlendColor(beforeColor, afterColor, percent);
    }
}
