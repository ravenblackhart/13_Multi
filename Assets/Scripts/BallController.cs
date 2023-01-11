using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float Speed = 0.01f;
    private bool moveBall = false;
    private Vector3 direction = Vector3.left + Vector3.up + Vector3.left ;

    private Vector3 startPos;

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

        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, 0.5f))
        {
            PlayerController player = hit.transform.GetComponentInChildren<PlayerController>();
            Vector3 incomingVec = hit.point - transform.position;
            Vector3 reflectVec = Vector3.Reflect(incomingVec, hit.normal);
            
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

    private void Restart()
    {
        transform.position = startPos;
        moveBall = false;
    }

}
