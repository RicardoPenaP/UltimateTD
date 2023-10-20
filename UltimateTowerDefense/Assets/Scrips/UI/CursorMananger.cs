using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMananger : Singleton<CursorMananger>
{
    protected override void Awake()
    {
        base.Awake();
        if (CursorMananger.Instance == this)
        {
            DontDestroyOnLoad(transform.parent.gameObject);
        }        
    }

    private void Start()
    {
        Cursor.visible = false;
        if (Application.isPlaying)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    private void Update()
    {
        Vector3 cursorPos = Input.mousePosition;
        transform.position = cursorPos;
        
        Cursor.visible = !MouseIsIntoTheViewport();


    }

    private bool MouseIsIntoTheViewport()
    {
        return (Input.mousePosition.x >= 0 && Input.mousePosition.x <= Screen.width) && (Input.mousePosition.y >= 0 && Input.mousePosition.y <= Screen.height);
    }
}
