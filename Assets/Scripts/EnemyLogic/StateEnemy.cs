using UnityEngine;
using UnityEngine.AI;

namespace EnemyLogic
{
    public abstract class StateEnemy
    {
        protected EnemyBase _enemyBase;
        protected IEnemyStateSwitcher _enemyStateSwitcher;
        protected NavMeshAgent _agent;
        protected StateEnemy(EnemyBase enemyBase, NavMeshAgent agent, IEnemyStateSwitcher enemyStateSwitcher)
        {
            _enemyBase = enemyBase;
            _agent = agent;
            _enemyStateSwitcher = enemyStateSwitcher;
        }
        
        public abstract void Start();
        public abstract void Stop();
        public abstract void Patrol();
        public abstract void Attack(Transform targetPosition);
        protected abstract void MoveToTarget();
        protected float GetDistance(Vector3 targetPosition)
        {
            float dist = Vector2.Distance(_enemyBase.CurrentPosition, targetPosition);
            return dist;
        }
    }
}