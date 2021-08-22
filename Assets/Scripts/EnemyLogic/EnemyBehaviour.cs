using System.Collections.Generic;
using System.Linq;
using EnemyLogic;
using PlayerLogic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyBehaviour : MonoBehaviour, IEnemyStateSwitcher
{
    [SerializeField] 
    private SoundsOfEnemies soundsOfEnemies;

    private TurnInDirection _turnInDirection;
    private EnemyBase _enemyBase;
    private StateEnemy _currentStateEnemy;
    private List<StateEnemy> _allStates;
    private NavMeshAgent _agent;
    private void Start()
    {
        Initialize();
        StartPatrol();
    }

    private void Initialize()
    {
        _turnInDirection = GetComponent<TurnInDirection>();
        NevMeshInit();
        AllStatesInit();
        GameManager.Instance.AddEnemy(this);
    }

    private void AllStatesInit()
    {
        _enemyBase = new EnemyBase(transform, this);
        
        _allStates = new List<StateEnemy>()
        {
            new PatrolState(_enemyBase, _agent,this),
            new AttackState(_enemyBase, _agent,this)
        };
    }

    private void StartPatrol()
    {
        _currentStateEnemy = _allStates[0];
        Patrol();
    }
    
    private void NevMeshInit()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }
    
    public void SwitchState<T>() where T : StateEnemy
    {
        var newState = _allStates.FirstOrDefault(s=> s is T);
        _currentStateEnemy?.Stop();

        if (newState == null) return;
        newState.Start();
        _currentStateEnemy = newState;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerBehaviour player))
        {
            Attack(player);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerBehaviour player))
        {
            Patrol();
        }
    }
    
    private void Attack(PlayerBehaviour target)
    {
        SwitchState<AttackState>();
        _turnInDirection.SetStatus(true);
        _currentStateEnemy.Attack(target.transform);
        soundsOfEnemies.UpdateState(true);
    }

    public void Patrol()
    {
        SwitchState<PatrolState>();
        _turnInDirection.SetStatus(false);
        _currentStateEnemy.Patrol();
        soundsOfEnemies.UpdateState(false);
    }

    private void OnDestroy()
    {
        GameManager.Instance.Delete();
    }
}