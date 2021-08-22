using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyLogic
{
    public class AttackState: StateEnemy
    {
        public AttackState(EnemyBase enemyBase, NavMeshAgent agent, IEnemyStateSwitcher enemyStateSwitcher) 
            : base(enemyBase, agent, enemyStateSwitcher)
        {
            _enemyBase = enemyBase;
            _enemyStateSwitcher = enemyStateSwitcher;
            _agent = agent;
        }
        
        public override void Start() { }

        public override void Stop() { }

        public override void Patrol() { }
        public override async void Attack(Transform targetPosition)
        {
            while (true)
            {
                if(targetPosition == null) break;
                if(GetDistance(targetPosition.position) > 3f) break;
                Debug.Log($"Атаку цель: {targetPosition}");
                _agent.destination = targetPosition.position;
                await Task.Delay(1000); 
            }
            _enemyBase.EnemyBehaviour.Patrol();
        }

        protected override void MoveToTarget() { }

    }
}