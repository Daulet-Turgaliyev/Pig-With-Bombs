using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace EnemyLogic
{
    public class PatrolState : StateEnemy
    {
        private readonly List<Vector3> _pointsOnRoute = new List<Vector3>
        {
            new Vector3(-4.5f, 6.3f, 0),
            new Vector3(-4.5f, -3.2f, 0),
            new Vector3(9.5f, -3.2f, 0),
            new Vector3(9.5f, 6.3f, 0),
            new Vector3(7.7f, -1.4f, 0),
            new Vector3(-4.6f, 0.15f, 0),
            new Vector3(1.3f, 2.5f, 0)
        };

        private Action OnEndMoving = () => { };
        private Vector3 _currentTarget;

        public PatrolState(EnemyBase enemyBase, NavMeshAgent agent, IEnemyStateSwitcher enemyStateSwitcher)
            : base(enemyBase, agent, enemyStateSwitcher)
        {
            _enemyBase = enemyBase;
            _enemyStateSwitcher = enemyStateSwitcher;
            _agent = agent;
        }

        public override void Start()
        {
            OnEndMoving += () => {  _enemyBase.EnemyBehaviour.Patrol(); };
        }

        public override void Stop()
        {
            OnEndMoving = null;
        }

        public override void Patrol()
        {
            int randomPoint = Random.Range(0, _pointsOnRoute.Count);
            _currentTarget = _pointsOnRoute[randomPoint];
            
            if(_agent == null) return;
            if (_agent.isActiveAndEnabled)
            {
                _agent.destination = _currentTarget;
            }
            
            MoveToTarget();
        }

        public override void Attack(Transform targetPosition) { }

        protected override async void MoveToTarget()
        {
            while (GetDistance(_currentTarget) > .5f)
            {
                await Task.Delay(1000);
            }

            OnEndMoving?.Invoke();
        }
    }
}