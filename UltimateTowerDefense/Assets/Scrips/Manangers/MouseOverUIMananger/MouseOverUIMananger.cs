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
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        for (int i = 0; i < raycastResults.Count; i++)
        {
            if (raycastResults[i].gameObject.GetComponent<IgnoredUI>())
            {
                raycastResults.RemoveAt(i);
                i--;
            }
        }

        mouseOverUI = raycastResults.Count > 0;
    }
}
