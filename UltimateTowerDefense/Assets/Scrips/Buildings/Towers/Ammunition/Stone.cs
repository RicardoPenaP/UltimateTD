using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour,IAmmunition
{
    [Header("Stone")]
    [SerializeField,Range(0f,20f)] float damageRadius = 5f;
    [Tooltip("The amount of time in seconds that will take to the stone travel to reach the target")]
    [SerializeField, Min(1f)] private float timeOfTravel = 1f;
    [Tooltip("Is the percentage of the distance that will translate to the max height of the curve")]
    [SerializeField,Range(0f,100f)] private float maxHeightPercentage = 50;
    [Tooltip("Is the representaive curve off the movement")]
    [SerializeField] private AnimationCurve heightCurve;
    
    private Vector3 startPos;
    private Vector3 finalPos;
    private float maxY;

    private int damage;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
    private void OnTriggerEnter(Collider other)
    {        
        Explode();
    }

    private void Explode()
    {
        Collider[] reachedObjects = Physics.OverlapSphere(transform.position, damageRadius);

        for (int i = 0; i < reachedObjects.Length; i++)
        {
            reachedObjects[i].GetComponent<EnemyDamageHandler>()?.TakeDamage(damage);
        }

        DestroyBehaviour();
    }

    public void SetTarget(Transform newDirection)
    {
        finalPos = new Vector3(newDirection.position.x,0,newDirection.position.z);
        startPos = transform.position;
        maxY = (Vector3.Distance(startPos, finalPos) * maxHeightPercentage) /100;
        StartCoroutine(ParableMovementRoutine());        
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    
    private void DestroyBehaviour()
    {
        //Destroy Behaviour
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private IEnumerator ParableMovementRoutine()
    {
        float elapsedTime = 0;  
        
        while (elapsedTime < timeOfTravel)
        {
            elapsedTime += Time.deltaTime;
            Vector3 newPos = new Vector3();
            float deltaT = elapsedTime / timeOfTravel;
            float heightT = heightCurve.Evaluate(deltaT);
            newPos.x = Mathf.Lerp(startPos.x, finalPos.x, deltaT);
            newPos.y = Mathf.Lerp(startPos.y, maxY, heightT);
            newPos.z = Mathf.Lerp(startPos.z, finalPos.z, deltaT);
                      
            transform.position = newPos;           
            yield return null;
        }

        Explode();

    }

    
}
