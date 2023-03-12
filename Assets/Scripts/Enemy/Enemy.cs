using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : EnemyManager
{
    [SerializeField] private string waypointsTag = "WaypointRoom1";

    [SerializeField] private List<Transform> waypoints;

    [SerializeField] private LayerMask detectionLayer;
    [SerializeField] private float closeViewRange = 2f;
    [SerializeField] private float farViewRange = 5f;
    [Range(0f, 360f)] [SerializeField] private float farViewAngle = 40f;
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private float speed = 3f;
    
    private SpriteRenderer _spriteRenderer;
    private Vector3 _target;
    private int _currentWaypointIndex = 0;
    private Transform _playerTarget;
    
    public new void Start()
    {
        base.Start();

        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        waypoints = GameObject.FindGameObjectsWithTag(waypointsTag).Select(x => x.transform).ToList();
        _target = new Vector3(0, 0, 0);
        _currentWaypointIndex = GetRandWaypointIndex();
        _playerTarget = GameObject.FindGameObjectWithTag(playerTag).transform;
    }

    void Update()
    {
        State.RunCurrentState();
        Debug.DrawRay(transform.position, _target - transform.position, Color.yellow);
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, speed * Time.deltaTime);
    }

    private void ChangeTarget(Vector3 newTarget)
    {
        _target = newTarget;
        LookAtTarget();
    }

    private void LookAtTarget()
    {
        Vector2 lookDirection = (_target - transform.position).normalized;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
    }

    public override bool CheckPlayerInArea() 
    {
        if (Physics2D.OverlapCircle(transform.position, closeViewRange, detectionLayer.value)) return true;

        if (Physics2D.OverlapCircle(transform.position, farViewRange, detectionLayer.value))
        {
            Vector2 directionToTarget = (_playerTarget.position - transform.position).normalized;
            return (Vector2.Angle(transform.up, directionToTarget) <= farViewAngle / 2);
        }

        return false;
    }

    public override void Patrol()
    {
        CheckWaypoints();
    }

    public override void Chase()
    {
        ChangeTarget(_playerTarget.position);
    }
    
    void CheckWaypoints()
    {
        if (waypoints.Count <= 0) return;
        
        ChangeTarget(waypoints[_currentWaypointIndex].position);
        const float TOLERANCE = 10e-6f;
        if (Math.Abs(transform.position.x - _target.x) < TOLERANCE && Math.Abs(transform.position.y - _target.y) < TOLERANCE)
        {
            _currentWaypointIndex = GetRandWaypointIndex();
        }
    }

    bool ObjectDetected(Vector2 targetPosition) 
    {
        Vector2 direction = ((Vector2)transform.position - targetPosition).normalized;        
        RaycastHit2D hit = Physics2D.CircleCast(targetPosition, _spriteRenderer.bounds.size.x / 2, direction);

        return !(hit.collider == null || hit.collider.CompareTag("Enemy"));
    }

    int GetRandWaypointIndex()
    {
        List<int> indexes = Enumerable.Range(0, waypoints.Count).ToList();
        indexes = indexes.OrderBy(_ => Guid.NewGuid()).ToList();
        
        foreach (int index in indexes)
        {
            if (index != _currentWaypointIndex && !ObjectDetected(waypoints[index].position))
            {
                return index;
            }
        }

        return _currentWaypointIndex;
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, closeViewRange);
        
        Gizmos.color = Color.white;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, farViewRange);

        Vector3 angl1 = DirFromAngl(-transform.eulerAngles.z, -farViewAngle * farViewRange);
        Vector3 angl2 = DirFromAngl(-transform.eulerAngles.z, farViewAngle * farViewRange);

        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, transform.position + angl1 * farViewRange);
        Gizmos.DrawLine(transform.position, transform.position + angl2 * farViewRange);
    }

    private Vector2 DirFromAngl(float eulerYm, float angleDegrees)
    {
        angleDegrees += eulerYm;
        return new Vector2(Mathf.Sin(angleDegrees * Mathf.Deg2Rad), Mathf.Cos(angleDegrees * Mathf.Deg2Rad));
    }
    
    
}
