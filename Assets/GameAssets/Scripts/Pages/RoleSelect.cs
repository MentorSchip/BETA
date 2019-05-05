using UnityEngine;

public class RoleSelect : MonoBehaviour
{
    [SerializeField] UserRole role;
    public UserRoleEvent OnRoleSelect;

    public void OnSelect()
    {
        OnRoleSelect.Invoke(role);
    }
}
