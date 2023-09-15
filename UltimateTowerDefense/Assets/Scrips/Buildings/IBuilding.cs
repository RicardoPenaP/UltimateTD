using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BuildingInterface
{
    public struct BuildingInfo
    {
        public string name;        
        public int currentLevel;
        public int currentAttackDamage;
        public float currentAttackRatio;
        public float currentAttackRange;
        public int currentUpgradeGoldCost;
        public int sellCost;
    }

    public delegate void OnSellDelegate();

    public interface IBuilding
    {
        public BuildingInfo GetBuildingInfo();
        public Sprite GetBuildingIcon();
        public string GetBuildingDescription();
        public void LevelUp();
        public void SellBuilding();
        public void SubscribeToOnSell(OnSellDelegate onSell);
    }
}

