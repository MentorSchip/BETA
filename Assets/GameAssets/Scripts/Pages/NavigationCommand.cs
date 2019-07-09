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
    
    public OrderedEvent[] OnFirstEntry;
    public UnityEvent OnEnterAny;       
    public UnityEvent OnEnterForward;   
    public UnityEvent OnEnterBack;      
    public UnityEvent OnExitAny;
    public UnityEvent OnExitForward;
    public UnityEvent OnExitBack;

    SequenceUtility sequence = new SequenceUtility();
    bool hasEntered = false;

    // Enter state (either normally or via Back button)
    public void Enter(bool isForward)
    {
        //Debug.Log("Entering " + gameObject.name);
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

        if (!hasEntered)
        {
            sequence.Begin(OnFirstEntry);
            hasEntered = true;
        }

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
