using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform myPointer;

    private void Awake()
    {
        myPointer = GetComponentInChildren<CameraPointer>().transform;
    }
}
