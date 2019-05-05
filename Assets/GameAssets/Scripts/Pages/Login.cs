using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

// [Refactor] delegate UI display methods, possibly share with CustomInputField
public class Login : MonoBehaviour
{
    public DynamicText dynamicText = new DynamicText();

    [SerializeField] InputField newNameInput;
    CustomInputField inputField;
    [SerializeField] Dropdown dropdown;
    int selectedIndex;
    bool isNewName;

    const int PROMPT = 0;

    List<UserData> allUsers { get { return PersistentDataManager.instance.UserData; } }
    UserData activeUser;

    public UnityEvent OnNewUserCreated;
    public UnityEvent OnStudentLogin;
    public UnityEvent OnMentorLogin;
    public UnityEvent OnChampLogin;

    void Start()
    {
        inputField = newNameInput.gameObject.GetComponent<CustomInputField>();
        PersistentDataManager.instance.Initialize();

        var loadResult = PersistentDataManager.instance.Load();
        Debug.Log("Load successful? " + loadResult);

        Initialize();
    }

    public void Initialize()
    {
        selectedIndex = 0;
        isNewName = true;

        dynamicText.RefreshText(isNewName);
        dynamicText.RefreshStyle(isNewName);

        PopulateList();
    }

    // Read values from external source
    public void PopulateList()
    {
        List<string> allNames = new List<string>();
        allNames.Add(dynamicText.prompt);

        foreach(UserData user in allUsers)
            allNames.Add(user.Name);

        dropdown.ClearOptions();
        dropdown.AddOptions(allNames);

        // Refresh value of selectedQuestion so data saves correctly
        DropdownIndexChanged(selectedIndex);
    }

    public void DropdownIndexChanged(int index)
    {
        selectedIndex = index;

        isNewName = (index == PROMPT);
        inputField.SetActive(isNewName);
        //Debug.Log("Dropdown index changed to index: " + index);

        if (!isNewName)
            activeUser = allUsers[index - 1];             

        dynamicText.RefreshText(isNewName);
        dynamicText.RefreshStyle(isNewName);
    }

    public void OnConfirm()
    {
        //Debug.Log("is valid name: " + IsValidNameInput(newNameInput.text) + ", " + newNameInput.text + ", is new name: " + isNewName); 
        if (isNewName)
        {
            if (IsValidNameInput(newNameInput.text))
            {
                activeUser = new UserData();
                activeUser.Name = newNameInput.text;
                allUsers.Add(activeUser);
                OnNewUserCreated.Invoke();
            }
            else return;
        }
        else LoginComplete();
    }

    // Listens for UnityEvent
    public void SetRole(UserRole role)
    {
        activeUser.SetRole(role);
        LoginComplete();
    }

    bool IsValidNameInput(string userName)
    {
        if (userName == null) return false;
        else if (userName == "" || 
                userName == ">>" ||
                userName == "Example Student" || 
                userName == "Enter name") return false;
        else return true;
    }

    public void LoginComplete()
    {
        PersistentDataManager.instance.Save(activeUser);

        //Debug.Log(activeUser.GetRole().ToString());
        switch (activeUser.GetRole())
        {
            case UserRole.Student: OnStudentLogin.Invoke(); break;
            case UserRole.Mentor: OnMentorLogin.Invoke(); break;
            case UserRole.Champ: OnChampLogin.Invoke(); break;
            default: Debug.LogError("Invalid user role"); break;
        }
    }
}
