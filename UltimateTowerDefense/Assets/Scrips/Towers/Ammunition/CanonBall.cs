using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBall : MonoBehaviour
{
    [Header("Canon Ball")]
    [SerializeField,Min(0f)] private float movementSpeed = 30f;

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.position += transform.right * movementSpeed * Time.deltaTime;
    }

    public void SetMovementDirection(Vector3 newDirection)
    {
        transform.right = newDirection;
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyController enemy = other.GetComponent<EnemyController>();

        if (enemy)
        {
            //Enemy Take Damage
        }

        //Destroy Behaviour
    }
}
