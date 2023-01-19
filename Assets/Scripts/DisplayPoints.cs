using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayPoints : MonoBehaviour
{
    private TMP_Text pointsText;
    private PlayerUI _playerUI;
    
    void Start()
    {
        _playerUI = GetComponent<PlayerUI>();
        pointsText = GetComponentInChildren<TMP_Text>();
    }

    void Update()
    {
        pointsText.text = _playerUI.points.ToString();
    }
}
