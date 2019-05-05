using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Scroll : MonoBehaviour
{
    public UnityEvent OnOpenBegin;
    public UnityEvent OnOpenEnd;
    public UnityEvent OnCloseBegin;
    public UnityEvent OnCloseEnd;

    // Used for opening screen scroll entering for 2nd+ time
    public UnityEvent OnReturn;


    public void OpenBegin()
    {
        OnOpenBegin.Invoke();
    }

    public void CloseBegin()
    {
        OnCloseBegin.Invoke();
    }

    public void OpenComplete()
    {
        OnOpenEnd.Invoke();
    }

    public void CloseComplete()
    {
        //Debug.Log("Scroll closed");
        OnCloseEnd.Invoke();
    }

    public void Return()
    {
        OnReturn.Invoke();
    }
}
