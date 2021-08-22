using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMesh))]
public class TurnInDirection : MonoBehaviour
{
    private bool _isAngry;
    private Vector3 _nextWaypoint;
    
    private NavMeshAgent _agent;
    
    [SerializeField]
    private SpriteRenderer currentSprite;
    [SerializeField]
    private Sprite[] avatars;
    
    [SerializeField]
    private Sprite[] angryAvatars;
    
    private int _direction;
    
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    private void Update()
    {
        if (!_agent.hasPath) return;
        if (_nextWaypoint == _agent.path.corners[1]) return;
        RotateToPoint(_agent.path.corners[1]);
        _nextWaypoint = _agent.path.corners[1];
    }

    public void SetStatus(bool newStatus)
    {
        _isAngry = newStatus;
        if (_agent == null) return;
        if (_agent.hasPath == false) return;
        RotateToPoint(_agent.path.corners[1]);
    }
    
    private void RotateToPoint(Vector3 targetPoint)
    {
        Vector3 targetVector = targetPoint - transform.position;

        if (Mathf.Abs(targetVector.x) >= Mathf.Abs(targetVector.y))
            _direction = targetVector.x > 0 ? 0 : 1;
        else
            _direction = targetVector.y > 0 ? 3 : 2;

        if (_isAngry)
            currentSprite.sprite = angryAvatars[_direction];
        else
            currentSprite.sprite = avatars[_direction];
    }
}