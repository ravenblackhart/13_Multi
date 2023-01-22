using System;
using System.Collections;
using System.Collections.Generic;
using Alteruna;
using Alteruna.Trinity;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private Multiplayer mp;
    
    public float Speed = 0.01f;
    private bool moveBall = false;
    private Vector2 direction = Vector2.left + Vector2.up + Vector2.left ;

    private Vector3 startPos;

    private Vector2 dir;
    private Vector2 norml;
    
    void RestartBallPositionRPC(ushort fromUser, ProcedureParameters parameters, uint callId, ITransportStreamReader processor) {
        RestartBallPosition();
    }
    
    void MoveBallRPC(ushort fromUser, ProcedureParameters parameters, uint callId, ITransportStreamReader processor) {
        MoveBall();
    }
    
    void Start()
    {
        startPos = transform.position;
        mp.RegisterRemoteProcedure("RestartBallPositionRPC", RestartBallPositionRPC);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Invoke on all other clients
            mp.InvokeRemoteProcedure("RestartRPC", UserId.All, null);
            // Invoke localy
            RestartBallPosition();
        }

        Vector2 pos = transform.position;

        RaycastHit2D[] result = new RaycastHit2D[5];
        int numberOfHits = Physics2D.RaycastNonAlloc(pos, direction, result, 1f);
        
        if (numberOfHits > 0) {
            foreach (RaycastHit2D hit in result) {
                if (!hit ) {
                    break;
                }
                
                PlayerController2D player = hit.transform.GetComponent<PlayerController2D>();
                Vector2 incomingVec = hit.point - pos;
                Vector2 reflectVec = Vector2.Reflect(incomingVec, hit.normal);

                dir = direction;
                norml = hit.normal;

                if (player) {
                    direction = reflectVec;
                }

                WallController wall = hit.transform.GetComponent<WallController>();
                if (wall) {
                    if (wall.deathWall) {
                        // Only the host can decide of the scoring and if the game is over
                        if (mp.Me.Index == 0) 
                        {
                            wall.IncreasePoints();
                            
                            mp.InvokeRemoteProcedure("RestartBallPositionRPC", UserId.All, null);
                            RestartBallPosition();
                        }

                        return;
                    }

                    direction = reflectVec;
                }
            }
        }

        if (moveBall)
        {
            Vector3 dir = direction.normalized * Time.deltaTime;
            dir *= Speed;
            transform.Translate(dir, Space.Self);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            mp.InvokeRemoteProcedure("MoveBallRPC", UserId.All, null);
            MoveBall();
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(dir.x, dir.y, 0) * 5);
        
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(norml.x, norml.y, 0) * 5);
    }

    private void MoveBall() 
    {
        moveBall = true;
    }
    
    public void RestartBallPosition()
    {
        transform.position = startPos;
        moveBall = false;
    }

}
