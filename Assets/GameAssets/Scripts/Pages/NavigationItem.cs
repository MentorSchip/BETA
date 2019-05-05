using UnityEngine;

// Attach to scene objects that are activated/deactivated with navigation commands
// but are not themselves commands (e.g. the Island scene)
public class NavigationItem : MonoBehaviour
{
    public bool ignoreSpecial;

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
