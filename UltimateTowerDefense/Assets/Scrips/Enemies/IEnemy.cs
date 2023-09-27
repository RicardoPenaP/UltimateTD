using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemiesInterface
{
    public enum EnemyState { None, Walking, CastingSkill,Attacking }
    public interface IEnemy
    {
        public void InitializeEnemy(float attackRange, int damageToStronghold);
    }
}

