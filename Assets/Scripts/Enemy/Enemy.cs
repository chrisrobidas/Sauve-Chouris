using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private string limitPointTag = "LimitPointRoom1";

    private List<Transform> _limitPoints;
    private Vector2 _minPos = new(float.MaxValue, float.MaxValue);
    private Vector2 _maxPos = new(float.MinValue, float.MinValue);
    
    void Start()
    {
        _limitPoints = new List<Transform>();
        // limitPoints = GameObject.FindGameObjectWithTag(limitPointTag).GetComponents<Transform>().ToList();
        var pointObjs = GameObject.FindGameObjectsWithTag(limitPointTag);

        foreach (var obj in pointObjs)
        { 
            _limitPoints.Add(obj.transform);
        }
        
        foreach (var point in _limitPoints)
        {
            if (point.position.x < _minPos.x)
                _minPos.x = point.position.x;
            if (point.position.y < _minPos.y)
                _minPos.y = point.position.y;
            if (point.position.x > _maxPos.x)
                _maxPos.x = point.position.x;
            if (point.position.y > _maxPos.y)
                _maxPos.y = point.position.y;
        }
    }

    void Update()
    {
        
    }
}
