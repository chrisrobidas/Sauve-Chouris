using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Echolocation : MonoBehaviour
{
    [SerializeField]
    float speed = 1f;
    [SerializeField]
    float maxRange = 5f;
    [SerializeField]
    SpriteRenderer echoSprite;

    float range = 0f;
    bool echoActive = false;
    List<Collider2D> collidersHit = new List<Collider2D> ();
    private void Start()
    {
        echoSprite.enabled = false;
    }

    private void Update()
    {
        if (range > maxRange)
        {
            echoActive = false;
            range = 0f;
            collidersHit.Clear();
            echoSprite.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !echoActive)
        {
            echoActive = true;
        }

        if (echoActive)
        {
            range += speed * Time.deltaTime;
            echoSprite.enabled = true;
            echoSprite.transform.localScale = new Vector3(range, range);
            EchoLocalize(range);
        }
    }

    public void EchoLocalize(float radius)
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, radius * 2f, new Vector2(0, 0));
        foreach (RaycastHit2D hit in hits)
        {
            if (!collidersHit.Contains(hit.collider))
            {
                collidersHit.Add(hit.collider);
                Debug.Log("Hit object at x: " + hit.transform.position.x + ", y: " + hit.transform.position.y);
            }
        }
    }
}
