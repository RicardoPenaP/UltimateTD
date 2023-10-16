using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace EnemiesInterface
{
    public enum EnemyState { None, Walking, CastingSkill,Attacking,Victory }
    public interface IEnemy
    {
        public void InitializeEnemy(float attackRange, int damageToStronghold);
        public void SubscribeToUpdateSkillCooldownUI(Action<float, float> OnUpdateSkillCooldownUIAction);
    }
}

