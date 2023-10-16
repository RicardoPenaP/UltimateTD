using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MenuAnimationHelper : MonoBehaviour
{
    public event Action OnOpenAnimationStarted;
    public event Action OnOpenAnimationFinished;
    public event Action OnCloseAnimationStarted;
    public event Action OnCloseAnimationFinished;

    public void OpenAnimationStarted()
    {
        OnOpenAnimationStarted?.Invoke();        
    }
    public void OpenAnimationFinished()
    {
        OnOpenAnimationFinished?.Invoke();
    }
    public void CloseAnimationStarted()
    {
        OnCloseAnimationStarted?.Invoke();
    }
    public void CloseAnimationFinished()
    {
        OnCloseAnimationFinished?.Invoke();
    }
}
