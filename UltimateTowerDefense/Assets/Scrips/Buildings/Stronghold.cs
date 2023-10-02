using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stronghold : MonoBehaviour
{
    private Vector2Int coordinates;

    public Vector2Int Coordinates { get { return coordinates; } }

    private void Awake()
    {
        coordinates.x = Mathf.RoundToInt(transform.position.x);
        coordinates.y = Mathf.RoundToInt(transform.position.z);
    }
}
