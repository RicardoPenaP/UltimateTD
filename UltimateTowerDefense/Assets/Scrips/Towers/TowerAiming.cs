using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAiming : MonoBehaviour
{
    private ITowerCanon myCanon;

    private void Awake()
    {
        myCanon = GetComponentInChildren<ITowerCanon>();
    }
}
