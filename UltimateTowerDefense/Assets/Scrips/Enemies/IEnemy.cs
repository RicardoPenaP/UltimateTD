using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IEnenmy
{
    public enum EnemyState { None, Walking, Attacking }
    public interface IEnemy
    {
        
        public void ResetWalkthroughPath();
        public void SetPath(List<Tile> path);
    }
}

