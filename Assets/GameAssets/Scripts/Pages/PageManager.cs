using UnityEngine;
using System.Collections.Generic;

public class PageManager : MonoBehaviour
{
    public List<NavigationCommand> commands = new List<NavigationCommand>();  // Make private when no longer debugging
    //List<Command> commands = new List<Command>();

    public void Navigate(NavigationCommand page)
    {
        if (commands.Count > 0)
        {
            commands[commands.Count-1].Exit(page, true);
            CheckAnimationTrigger(page, commands[commands.Count-1]);
        }

        commands.Add(page);
        //commands.Add(new Command(page, PersistentDataManager.instance.currentUser));
        page.Enter(true);

        if (page.isEmptyState)
        {
            if (page.skipToCommand == null)
            {
                Debug.LogError("Null reference in skipToCommand for empty navigation command!");
                return;
            }

            Navigate(page.skipToCommand);
        }
    }

    // Open or close the scroll when transitioning between a page that has a scroll and one that does not
    private void CheckAnimationTrigger(NavigationCommand toPage, NavigationCommand fromPage)
    {
        if (toPage.isEmptyState)
            return;

        if (toPage.containsAnimatedObject && !fromPage.containsAnimatedObject)
        {
            //Debug.Log("Transitioning from close to open, opening scroll");
            TriggerAnimation(toPage.animatedObject, true);
        }
        else if (!toPage.containsAnimatedObject && fromPage.containsAnimatedObject)
        {
            //Debug.Log("Transitioning from open to close, closing scroll");
            TriggerAnimation(toPage.animatedObject, false);
        }
        else if (toPage.animatedObject != fromPage.animatedObject)
        {
            TriggerAnimation(toPage.animatedObject, toPage.containsAnimatedObject);
        }
    }

    private void TriggerAnimation(Animator animated, bool value)
    {
        //Debug.Log("Triggering animation: " + animated.gameObject.name + " " + value);
        tempAnim = animated;
        tempValue = value;
        Invoke("DelayAnimation", .02f);   
    }

    // * Hack fix and sometimes causes short visual glitch, try to remove delay when underlying bug identified
    Animator tempAnim;
    bool tempValue;
    private void DelayAnimation()
    {
        if (tempAnim == null)
        {
            Debug.LogWarning("tempAnim unassigned, check if there is an animator attached to " + commands[commands.Count-1].gameObject.name);
            return;
        }

        tempAnim.SetBool("IsOpen", tempValue);
    }

    public void Back()
    {
        if (commands.Count <= 1)
            return;

        // Get references to current and prior command
        var currentCommand = commands[commands.Count-1];
        var priorCommand = commands[commands.Count-2];

        // Remove the latest command from the list 
        // [TBD] change this if Forward button added in future
        commands.RemoveAt(commands.Count-1);
        
        // Leave the current state (parameter = where to go next)
        currentCommand.Exit(priorCommand, false);

        if (priorCommand.isEmptyState)
        {
            Back();
            return;
        }

        priorCommand.Enter(false); 
        CheckAnimationTrigger(priorCommand, currentCommand); 
    }

    public void DisplayCurrent(bool isActive)
    {
        //Debug.Log(commands[commands.Count-1].name + " " + isActive);
        commands[commands.Count-1].DisplaySelf(isActive, true);
    }

    public void ClearHistory()
    {
        commands.Clear();
    }
}
