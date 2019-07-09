using UnityEngine;
using UnityEngine.Events;

public class ImageTrigger : MonoBehaviour
{
    [SerializeField] GameObject gameContainer;
    [SerializeField] GameObject trackable;
    [SerializeField] ScholarshipSet level;
    IGame game;

    [SerializeField] float waitTime = 1f;
    public UnityEvent OnActivate;
    public UnityEvent OnReactivate;
    public bool hasActivated = false;

    private void Awake()
    {
        game = gameContainer.GetComponent<IGame>();
        level.target = gameObject;
    }

    public void OnClick()
    {
        if (trackable.activeSelf)
            game.OnClickTrackable(gameObject);
    }

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
}
