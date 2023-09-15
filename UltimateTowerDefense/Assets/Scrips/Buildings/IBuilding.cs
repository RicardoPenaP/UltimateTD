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
        public float currentUpgradeGoldCost;
    }

    public interface IBuilding
    {

    }
}

