using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Echolocation : MonoBehaviour
{
    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EchoLocalize();
        }
    }

    public void EchoLocalize()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 50, new Vector2(0, 0));
        foreach (RaycastHit2D hit in hits)
        {
            Debug.Log("Hit object at x: " + hit.transform.position.x + ", y: " + hit.transform.position.y);
        }
    }
}
