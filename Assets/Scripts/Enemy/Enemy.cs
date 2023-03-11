using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : EnemyManager
{
    [SerializeField] private string waypointsTag = "WaypointRoom1";

    [SerializeField] private List<Transform> waypoints;

    [SerializeField] private float speed = 3f;

    [SerializeField] private LayerMask _detectionLayer;
    [SerializeField] private float _radiusRange = 2f;

    private int _currentWaypointIndex = 0;

    private SpriteRenderer _spriteRenderer;
    
    [SerializeField] private LayerMask detectionLayer;
    [SerializeField] private float closeViewAngle = 2f;
    [SerializeField] private float farViewAngle = 4f;
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private float speed = 3f;

    private Vector3 _target;
    private int _currentWaypointIndex = 0;
    // private bool _isChasing = false;
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
        var position = transform.position;
        Debug.DrawRay(position, _target - position, Color.blue);
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, speed * Time.deltaTime);
    }

    public override bool CheckPlayerInArea() => Physics2D.OverlapCircle(transform.position, closeViewAngle, detectionLayer.value);

    public override void Patrol()
    {
        CheckWaypoints();
    }

    public override void Chase()
    {
        _target = _playerTarget.position;
        Debug.Log("Chasing");
    }
    
    void CheckWaypoints()
    {
        if (_waypoints.Count <= 0) return;
        
        _target = _waypoints[_currentWaypointIndex].position;
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

        return !(hit.collider == null || hit.collider.tag == "Enemy");
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
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, closeViewAngle);
    }
    
    
}
