using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private string _waypointsTag = "WaypointRoom1";

    [SerializeField] private List<Transform> _waypoints;

    private int _currentWaypointIndex = 0;

    
    void Start()
    {
        _waypoints = GameObject.FindGameObjectsWithTag(_waypointsTag).Select(x => x.transform).ToList();
        _currentWaypointIndex = GetRandWaypointIndex();
    }

    void Update()
    {
        if (_waypoints.Count > 0)
        {
            Vector3 targetPosition = _waypoints[_currentWaypointIndex].position;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 1f * Time.deltaTime);
            float TOLERANCE = 10e-6f;
            if (Math.Abs(transform.position.x - targetPosition.x) < TOLERANCE && Math.Abs(transform.position.y - targetPosition.y) < TOLERANCE)
            {
                _currentWaypointIndex = GetRandWaypointIndex();
            }
        }
    }

    int GetRandWaypointIndex()
    {
        int randIndex = UnityEngine.Random.Range(0, _waypoints.Count);
        if (randIndex == _currentWaypointIndex)
        {
            return GetRandWaypointIndex();
        }
        else
        {
            return randIndex;
        }
    }
}
