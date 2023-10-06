using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindEffectOnWater : MonoBehaviour
{
    [Header("Wind Effect On Water")]
    [SerializeField,Min(0f)] private float speed = 1f;
    [SerializeField] private float maxSpeed;    
    [SerializeField] private float aceleration = 0.2f;
    [SerializeField] private float changeIntervalTime = 1f;

    private Vector2 direction = Vector2.down;
    private Material waterMaterial;

    private void Awake()
    {
        waterMaterial = GetComponent<MeshRenderer>().materials[1];
        StartCoroutine(ChangeSpeedRoutine());
    }

    private void Update()
    {
        MoveOffset();
    }

    private void MoveOffset()
    {
        waterMaterial.mainTextureOffset += direction * speed * Time.deltaTime;
    }

    private IEnumerator ChangeSpeedRoutine()
    {
        float acelerationMultiplier = 1;
        while (true)
        {
            if (speed >= maxSpeed)
            {
                acelerationMultiplier = - 1;
            }

            if (speed <= 0.5)
            {
                acelerationMultiplier = 1;
            }

            speed += aceleration * acelerationMultiplier;

            yield return null;
        }
    }
}
