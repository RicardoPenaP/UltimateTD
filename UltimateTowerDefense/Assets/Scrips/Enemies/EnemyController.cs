using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemiesInterface;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Controller")]
    [SerializeField] private EnemyData myData;
    
    private IEnemy myEnemyIA;
    [SerializeField]private UIHealthAndShieldBar myUI;

    private int currentHealth;
    private int currentMaxHealth;
    private int currentShield;
    private int currentMaxShield;

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
        myUI = GetComponentInChildren<UIHealthAndShieldBar>();
    }

    private void OnEnable()
    {
        
    }

    private void Start()
    {
        ResetEnemy();
    }

    private void OnDisable()
    {
        ResetEnemy();
        
    }

    private void ResetEnemy()
    {
        currentMaxHealth = myData.MaxHealth;
        currentHealth = currentMaxHealth;
        currentMaxShield = myData.MaxShield;
        currentShield = currentMaxShield;
        UpdateUI();
        canWalk = true;
        isAlive = true;
        transform.localPosition = Vector3.zero;
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
        UpdateUI();
    }

    private void UpdateUI()
    {
        myUI.UpdateBar(currentHealth, currentMaxHealth, currentShield, currentMaxShield);
    }


   
}
