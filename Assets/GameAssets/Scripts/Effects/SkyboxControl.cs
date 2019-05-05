using UnityEngine;
using UnityEngine.Events;

public class SkyboxControl : MonoBehaviour
{
    [SerializeField] Material skyboxOnEnable, skyboxOnDisable;
    //[SerializeField] GameObject arCamera;
    public UnityEvent OnEnabled;
    public UnityEvent OnDisabled;

    void OnEnable()
    {
        RenderSettings.skybox = skyboxOnEnable;
        //arCamera.SetActive(false);
        OnEnabled.Invoke();
    }
 
    void OnDisable()
    {
        RenderSettings.skybox = skyboxOnDisable;
        //arCamera.SetActive(true);
        OnDisabled.Invoke();
    }
}
