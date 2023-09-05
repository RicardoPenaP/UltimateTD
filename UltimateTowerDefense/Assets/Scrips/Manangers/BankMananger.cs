using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankMananger : MonoBehaviour
{
    [Header("Bank Mananger")]
    [SerializeField,Min(0)] private int maxAmountGold = 100000;
    [SerializeField,Min(0)] private int startingAmouuntGold = 250;
    [SerializeField] private int currentAmountGold;

    private void Start()
    {
        currentAmountGold = startingAmouuntGold;
    }

    public bool HaveEnoughGoldCheck(int amount)
    {
        return currentAmountGold >= amount;
    }

    public void SubtractGold(int amount)
    {
        if (!HaveEnoughGoldCheck(amount))
        {
            return;
        }

        currentAmountGold-= amount;
    }

    public void AddGold(int amount)
    {
        if (amount + currentAmountGold > maxAmountGold)
        {
            currentAmountGold = maxAmountGold;
        }
        else
        {
            currentAmountGold += amount;
        }
    }
}
