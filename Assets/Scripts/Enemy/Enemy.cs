using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private string _waypointsTag = "WaypointRoom1";

    [SerializeField] private List<Transform> _waypoints;

    private int _currentWaypointIndex = 0;

    [SerializeField] private SpriteRenderer _spriteRenderer;

    
    void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _waypoints = GameObject.FindGameObjectsWithTag(_waypointsTag).Select(x => x.transform).ToList();
        _currentWaypointIndex = GetRandWaypointIndex();
        
    }

    void Update()
    {
        if (_waypoints.Count > 0)
        {
            Vector3 targetPosition = _waypoints[_currentWaypointIndex].position;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 8f * Time.deltaTime);
            float TOLERANCE = 10e-6f;
            if (Math.Abs(transform.position.x - targetPosition.x) < TOLERANCE && Math.Abs(transform.position.y - targetPosition.y) < TOLERANCE)
            {
                _currentWaypointIndex = GetRandWaypointIndex();
            }
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
        List<int> indexes = Enumerable.Range(0, _waypoints.Count).ToList();
        indexes = indexes.OrderBy(_ => Guid.NewGuid()).ToList();

        foreach (int index in indexes)
        {
            if (index != _currentWaypointIndex && !ObjectDetected(_waypoints[index].position))
            {
                return index;
            }
        }

        return _currentWaypointIndex;
    }
}
