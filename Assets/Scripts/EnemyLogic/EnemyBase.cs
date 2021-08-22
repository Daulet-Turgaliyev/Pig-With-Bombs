using System;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyLogic
{
    public class EnemyBase
    {
        public readonly EnemyBehaviour EnemyBehaviour;
        private readonly Transform _enemyTransform;
        public Vector3 CurrentPosition
        {
            get
            {
                if(_enemyTransform == null) return Vector3.zero;
                return _enemyTransform.position;
            }
        }

        public EnemyBase(Transform transform, EnemyBehaviour enemyBehaviour)
        {
            _enemyTransform = transform;
            EnemyBehaviour = enemyBehaviour;
        }
    }
}