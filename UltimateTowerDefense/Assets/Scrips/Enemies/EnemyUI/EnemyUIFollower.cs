using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUIFollower : MonoBehaviour
{
    private EnemyDamageHandler myDamageHandler;

    private void Awake()
    {
        myDamageHandler = GetComponent<EnemyDamageHandler>();
    }

    private void Update()
    {
        UpdateUIPos();
    }

    private void UpdateUIPos()
    {
        transform.position = new Vector3(myDamageHandler.transform.position.x, transform.position.y, myDamageHandler.transform.position.z);
    }
}
