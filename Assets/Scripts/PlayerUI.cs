using System;
using System.Collections;
using System.Collections.Generic;
using Alteruna;
using UnityEngine;
using Avatar = Alteruna.Avatar;

public class PlayerUI : AttributesSync
{
    [SynchronizableField] public int points = 100;
    [SynchronizableField] public int dmg = 5;

    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private int selfLayer;
    
    
    private Avatar avatar;
    
    void Start()
    {
        avatar = GetComponentInParent<Avatar>();
        if (avatar.IsMe)
        {
            avatar.gameObject.layer = selfLayer;
        }
    }

    void Update()
    {
        if (!avatar.IsMe)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Raycast();
        }
    }

    private void Raycast()
    {
        if (Physics.Raycast(transform.position, transform.up, out RaycastHit hit, Mathf.Infinity, playerLayer))
        {
            PlayerUI playerUI = hit.transform.GetComponentInChildren<PlayerUI>();
            playerUI.DecreasePoints();
        }
        
    }

    private void DecreasePoints()
    {
        Debug.Log("Got shot");
        points -= dmg;
    }
}
