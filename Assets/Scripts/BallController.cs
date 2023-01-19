using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float Speed = 0.01f;
    private bool moveBall = false;
    private Vector2 direction = Vector2.left + Vector2.up + Vector2.left ;

    private Vector3 startPos;

    private Vector2 dir;
    private Vector2 norml;
    
    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }

        Vector2 pos = transform.position;

        RaycastHit2D hit = Physics2D.Raycast(pos, direction, 1f);
        if (hit.collider != null)
        {
            PlayerController2D player = hit.transform.GetComponent<PlayerController2D>();
            Vector2 incomingVec = hit.point - pos;
            Vector2 reflectVec = Vector2.Reflect(incomingVec, hit.normal);
            
            dir = direction;
            norml = hit.normal;
            
            if (player)
            {
                direction = reflectVec;
            }
            WallController wall = hit.transform.GetComponent<WallController>();
            if (wall)
            {
                if (wall.deathWall)
                {
                    wall.IncreasePoints();
                    Restart();
                    return;
                }
                direction = reflectVec;
            }
        }
            
        if (moveBall)
        {
            Vector3 dir = direction.normalized * Time.deltaTime;
            dir *= Speed;
            transform.Translate(dir, Space.Self);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            moveBall = true;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(dir.x, dir.y, 0) * 5);
        
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(norml.x, norml.y, 0) * 5);

    }

    private void Restart()
    {
        transform.position = startPos;
        moveBall = false;
    }

}
