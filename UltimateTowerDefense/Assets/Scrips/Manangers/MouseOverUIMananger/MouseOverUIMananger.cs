using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseOverUIMananger : Singleton<MouseOverUIMananger>
{
    private bool mouseOverUI = false;

    public bool MouseOverUI { get { return mouseOverUI; } }

    private void Update()
    {
        SetMouseOverUI();

    }

    private void SetMouseOverUI()
    {
        mouseOverUI = EventSystem.current.IsPointerOverGameObject();
    }
}
