using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewRange : MonoBehaviour
{
    private Transform circleTransform;

    private void Awake()
    {
        circleTransform = transform.GetChild(0);
    }

    public void SetCircleSize(float size)
    {
        circleTransform.localScale = new Vector3(size*2, size * 2, 1);        
    }
}
