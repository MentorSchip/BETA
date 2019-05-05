using UnityEngine;
using UnityEngine.Events;

public class SplashScreen : MonoBehaviour
{
    // Unable to set bool via UnityEvent, so using direct reference to animator to control state
    public Animator bottleAnimator;

    // Set starting state of application
    public PageManager pageManager;
    public NavigationCommand startingPage;

    public UnityEvent OnClick;

    void Awake()
    {
        bottleAnimator.SetBool("HasStarted", false);
    }

    void Start()
    {
        pageManager.ClearHistory();
        pageManager.Navigate(startingPage);
    }

    void Update()
    {
        if (bottleAnimator.GetBool("HasStarted") == false && Input.GetMouseButtonDown(0))
        {
            OnClick.Invoke();
            bottleAnimator.SetBool("HasStarted", true);
        }
    }
}
