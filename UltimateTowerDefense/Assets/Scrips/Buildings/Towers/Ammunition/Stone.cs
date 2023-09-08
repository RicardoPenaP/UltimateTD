using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour,IAmmunition
{
    [Header("Stone")]   
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

    public void SetMovementDirection(Vector3 newDirection)
    {
        finalPos = newDirection;
        startPos = transform.position;
        maxY = (Vector3.Distance(startPos, finalPos) * maxHeightPercentage) /100;
        StartCoroutine(ParaboleMovementRoutine());        
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyController enemy = other.GetComponent<EnemyController>();

        if (enemy)
        {
            enemy.TakeDamage(damage);
            DestroyBehaviour();
            return;
        }

        Tile tile = other.GetComponent<Tile>();
        if (tile)
        {
            DestroyBehaviour();
            return;
        }

        
    }

    private void DestroyBehaviour()
    {
        //Destroy Behaviour
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private IEnumerator ParaboleMovementRoutine()
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

            //Height calculation
            
            
                      
            transform.position = newPos;           
            yield return null;
        }
       
    }

    
}
