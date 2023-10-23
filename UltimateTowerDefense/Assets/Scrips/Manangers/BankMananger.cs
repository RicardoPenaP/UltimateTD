using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BankMananger : Singleton<BankMananger>
{
    [Header("Bank Mananger")]
    [SerializeField,Min(0)] private int maxAmountGold = 100000;
    private int startingAmouuntGold = 0;
    private int currentAmountGold;


    private TextMeshProUGUI goldText;

    protected override void Awake()
    {
        base.Awake();
        goldText = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Start()
    {
        switch (GameMode.GameModeOption)
        {
            case GameModeOptions.SingleRoad:
                startingAmouuntGold = 50;
                break;
            case GameModeOptions.DoubleRoad:
                startingAmouuntGold = 100;
                break;
            case GameModeOptions.TripleRoad:
                startingAmouuntGold = 150;
                break;
            case GameModeOptions.QuadRoad:
                startingAmouuntGold = 200;
                break;
            default:
                break;
        }
        currentAmountGold = startingAmouuntGold;
        UpdateBankUI();
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
        UpdateBankUI();
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
        UpdateBankUI();
    }

    private void UpdateBankUI()
    {
        goldText.text = $"{currentAmountGold}";
    }
}
