using System.Collections;
using System.Collections.Generic;
using Alteruna;
using TMPro;
using UnityEngine;
using Avatar = UnityEngine.Avatar;

public class WallController : AttributesSync
{
    [SerializeField] public bool deathWall = false;
    [SerializeField] private TMP_Text pointsText;

    [SynchronizableField] public int points = 0;
    [SerializeField] public int dmg = 1;

    void Update()
    {
        pointsText.text = points.ToString();
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
