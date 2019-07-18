/* using UnityEngine;
using UnityEngine.Events;

// DEPRECATE: OnClick covered by RaycastTrigger, all other functionality obsolete
public class ImageTrigger : MonoBehaviour
{
    //[SerializeField] GameObject gameContainer;
    [SerializeField] GameObject trackable;
    ScholarshipSet level;
    //IGame game;

    // [Obsolete]
    [SerializeField] float waitTime = 1f;
    public UnityEvent OnActivate;
    public UnityEvent OnReactivate;
    public bool hasActivated = false;

    private void Awake()
    {
        game = gameContainer.GetComponent<IGame>();
        level.target = gameObject;  // Pass reference to self to level -> ERROR: how does an image know its level?  No longer a SO...
    }

    public void OnClick()
    {
        //Debug.Log("Image trigger clicked!");
        if (trackable.activeSelf)
            game.OnClickTrackable();
    }

    // [Obsolete]
    public void ActivateNext()
    {
        //Debug.Log(gameObject.name + " activating next image...");
        Invoke("DelayActivation", waitTime);
    }

    private void DelayActivation()
    {
        OnActivate.Invoke();
    }

    public void Activate()
    {
        if (hasActivated)
            OnReactivate.Invoke();
        else
        {
            gameObject.SetActive(true);
            hasActivated = true;
        }
    }
}*/
