using System;
using System.Collections;
using System.Collections.Generic;
using Alteruna;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using Avatar = UnityEngine.Avatar;

public class WallController : AttributesSync
{
    [SerializeField] public bool deathWall = false;
    [CanBeNull][SerializeField] private TMP_Text pointsText;

    [SynchronizableField] public int points = 0;
    [SerializeField] public int dmg = 1;

    private UIManager _uiManager; 

    private void Awake()
    {
        _uiManager = FindObjectOfType<UIManager>(); 
    }

    void Update()
    {
        if (deathWall && pointsText != null)
        {
            pointsText.text = points.ToString();
        }
        
    }

    public void DecreasePoints()
    {
        points -= dmg;
    }

    public void IncreasePoints()
    {
        points += dmg;
        _uiManager.TriggerGameOver();
        
    }

    public void ResetPoints()
    {
        points = 0; 
    }
}
