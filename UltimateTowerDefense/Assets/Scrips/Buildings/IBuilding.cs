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
        public int currentLevelUpGoldCost;
        public int sellCost;
    }

    public delegate void OnSellDelegate();

    public interface IBuilding
    {
        public BuildingUIData GetBuildindUIInfo();
        public BuildingInfo GetBuildingInfo();       
        public void LevelUp();
        public void SellBuilding();
        public void SubscribeToOnSell(OnSellDelegate onSell);
    }
}

