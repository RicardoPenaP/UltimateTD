using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemiesInterface
{
    public enum EnemyState { None, Walking, Attacking }
    public interface IEnemy
    {
        public void SetPath(List<Tile> path);
        public void Die();
    }
}

