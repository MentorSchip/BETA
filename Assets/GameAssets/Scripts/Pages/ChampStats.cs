using UnityEngine;
using UnityEngine.UI;

public class ChampStats : MonoBehaviour
{
    [SerializeField] Scroll scroll;

    // Must all be of the same length
    [SerializeField] ProgressState[] demoStates;
    [SerializeField] int[] maximums;
    [SerializeField] Text[] progressTexts;

    int currentProgress;

    public void OnEnable()
    {
        Initialize();
    }

    private void Initialize()
    {
        scroll.OpenBegin();
        currentProgress = -1;
        Refresh();
    }

    public void Refresh()
    {
        currentProgress++;

        if (currentProgress >= demoStates.Length)
            currentProgress = 0;

        for (int i = 0; i < progressTexts.Length; i++)
            progressTexts[i].text = demoStates[currentProgress].values[i] + "/" + maximums[i];
    }
}

[System.Serializable]
public struct ProgressState
{
    public int[] values;
}
