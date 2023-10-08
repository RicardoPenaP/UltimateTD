using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stronghold : Singleton<Stronghold>
{
    private Vector2Int coordinates;

    public Vector2Int Coordinates { get { return coordinates; } set { coordinates = value; }  }

    public Transform GetStrongholdPos()
    {
        return transform;
    }
}
