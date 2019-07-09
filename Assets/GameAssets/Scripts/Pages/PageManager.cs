using UnityEngine;
using System.Collections.Generic;

public class PageManager : MonoBehaviour
{
     // Make private when no longer debugging
    public List<NavigationCommand> commands = new List<NavigationCommand>(); 

    public void Navigate(NavigationCommand page)
    {
        if (commands.Count > 0)
        {
            commands[commands.Count-1].Exit(page, true);
            CheckAnimationTrigger(page, commands[commands.Count-1]);
        }

        commands.Add(page);
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
            toPage.animatedObject.SetBool("IsOpen", true);
        }
        else if (!toPage.containsAnimatedObject && fromPage.containsAnimatedObject)
        {
            //Debug.Log("Transitioning from open to close, closing scroll");
            toPage.animatedObject.SetBool("IsOpen", false);
        }
        else if (toPage.animatedObject != fromPage.animatedObject)
        {
            toPage.animatedObject.SetBool("IsOpen", toPage.containsAnimatedObject);
        }
    }

    public void Back()
    {
        //Debug.Log("Back method reached...");
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
        //Debug.Log("DisplayCurrent method called: " + commands.Count);
        if (commands.Count <= 0)
            return;

        //Debug.Log(commands[commands.Count-1].name + " " + isActive);
        commands[commands.Count-1].DisplaySelf(isActive, true);
    }

    public void ClearHistory()
    {
        commands.Clear();
    }
}
