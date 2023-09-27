using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUI : MonoBehaviour
{   
    private void Awake()
    {
        UIHealthAndShieldBar myUIBar = GetComponentInChildren<UIHealthAndShieldBar>();
        GetComponentInParent<EnemyHealthHandler>().OnUpdateUI += myUIBar.UpdateBar;
    }
}
