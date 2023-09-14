using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemiesInterface;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Controller")]
    [SerializeField] private EnemyData myData;

    //[Header("Health Settings")]
    //[SerializeField] private int maxHealth = 100;

    //[Header("Movement Settings")]
    //[SerializeField] private float movementSpeed;
   
    //[SerializeField] private float distanceFromNextTileOffset = 0.05f;

    //[Header("Rewards Settings")]
    //[SerializeField,Min(0f)] private int goldReward = 25;

    //[Header("Damage Settings")]
    //[SerializeField] private int damageToStronghold = 1;

    
    private IEnemy myEnemyIA;

    private int currentHealth;
    private int currentShield;

    private bool isAlive = true;
    private bool canWalk;

    public float MovementSpeed { get { return myData.MovementSpeed; } }
    public bool CanWalk { get { return canWalk; } }
    public float DistanceFromNextTileOffset { get { return myData.DistanceFromNextileOffset; } }
    public int GoldReward { get { return myData.GoldReward; } }
    public int DamageToStronghold { get { return myData.DamageToStronghold; } }
    public bool IsAlive { get { return isAlive; } }

    private void Awake()
    {        
        myEnemyIA = GetComponent<IEnemy>();
    }

    private void OnEnable()
    {
        ResetEnemy();
    }

    private void OnDisable()
    {
        transform.localPosition = Vector3.zero; 
    }

    private void ResetEnemy()
    {
        currentHealth = myData.MaxHealth;
        currentShield = myData.MaxShield;
        canWalk = true;
        isAlive = true;
    }

    public void SetEnemyPath(List<Tile> newPath)
    {
        myEnemyIA.SetPath(newPath);
    }

    public void TakeDamage(int damageAmount)
    {
        if (!isAlive)
        {
            return;
        }

        if (currentShield > 0)
        {
            if (currentShield >= damageAmount)
            {
                currentShield -= damageAmount;
            }
            else
            {
                int leftDamage = damageAmount - currentShield;
                currentShield = 0;
                currentHealth -= leftDamage;
            }
        }
        else
        {
            currentHealth -= damageAmount;
        }
        
        if (currentHealth <= 0)
        {
            isAlive = false;
            currentHealth = 0;
            myEnemyIA.Die();
        }
    }


   
}
