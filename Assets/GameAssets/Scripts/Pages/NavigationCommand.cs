using UnityEngine;
using UnityEngine.Events;

public class NavigationCommand : MonoBehaviour
{
    public NavigationCommand supercommand;
    public NavigationCommand[] subcommands;
    public NavigationItem[] alsoActivate;

    public bool isEmptyState;
    public NavigationCommand skipToCommand;

    public bool containsAnimatedObject;
    public Animator animatedObject;
    
    public UnityEvent OnEnterAny;       
    public UnityEvent OnEnterForward;   
    public UnityEvent OnEnterBack;      
    public UnityEvent OnExitAny;
    public UnityEvent OnExitForward;
    public UnityEvent OnExitBack;

    // Enter state (either normally or via Back button)
    public void Enter(bool isForward)
    {
        DisplaySelf(true, false);
        EnterSupercommand(isForward);

        OnEnterAny.Invoke();

        if (isForward)
            OnEnterForward.Invoke();
        else
            OnEnterBack.Invoke(); 
    }

    // Leave state (either normally or via Back button)
    public void Exit(NavigationCommand newCommand, bool isForward)
    {
        foreach (NavigationCommand subcommand in subcommands)
            if (newCommand == subcommand)
                return;

        //Debug.Log("Exiting " + this.name);
        if (supercommand != null)
            supercommand.Exit(newCommand, isForward);
        
        DisplaySelf(false, false);

        OnExitAny.Invoke();

        if (isForward)
            OnExitForward.Invoke();
        else
            OnExitBack.Invoke();
    }

    public void DisplaySelf(bool isActive, bool allowIgnore)
    {
        gameObject.SetActive(isActive);

        foreach (var navItem in alsoActivate)
        {
            if (navItem.ignoreSpecial && allowIgnore)
                continue;
            else
                navItem.SetActive(isActive);
        }
    }

    // Recursively pass command up in hierarchy
    private void EnterSupercommand(bool isForward)
    {
        if (supercommand == null)
            return;

        supercommand.Enter(isForward);
    }
}
