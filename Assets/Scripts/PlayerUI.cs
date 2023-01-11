using System;
using System.Collections;
using System.Collections.Generic;
using Alteruna;
using UnityEngine;
using Avatar = Alteruna.Avatar;

public class PlayerUI : AttributesSync
{
    [SynchronizableField] public int points = 0;
    [SerializeField] public int dmg = 5;

    private Avatar avatar;
    
    void Start()
    {
        avatar = GetComponentInParent<Avatar>();
    }

    void Update()
    {
        if (!avatar.IsMe)
        {
            return;
        }
    }

    public void DecreasePoints()
    {
        points -= dmg;
    }

    public void IncreasePoints()
    {
        points += dmg;
    }
}
