using UnityEngine;

[CreateAssetMenu(menuName = "Playcraft/Utilities/Data Types/Object")]
public class CustomObject : ScriptableObject
{
    public GameObject Asset;
    [HideInInspector] public GameObject Instance;
    public bool ignoreSpecial;

    public void SetActive(bool isActive)
    {
        Instance.SetActive(isActive);
    }
}
