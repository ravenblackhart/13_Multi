using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;
using TMPro;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;


public class UIManager : MonoBehaviour
{
    [Header ("MainUI")]
    [SerializeField] private TMP_Text currentRoom; 
    [SerializeField] private TMP_Text leftScore;
    [SerializeField] private TMP_Text rightScore;
    [SerializeField] private TMP_Text leftLabel;
    [SerializeField] private TMP_Text rightLabel;
    [SerializeField] private TMP_Text pauseButton;
    [SerializeField] private TMP_Text infoText; 
    [SerializeField] private Button leaveButton;
    [SerializeField] private Button roomsButton; 

    [Header("RoomMenu")]
    [SerializeField] private Canvas roomMenu;

    [Header("PauseMenu")]
    [SerializeField] private Canvas pauseMenu;

    [Header("GameOverMenu")]
    [SerializeField] private Canvas gameOverMenu;
    
    
    [Header("Misc")]
    private Alteruna.Avatar _avatar;
    [SerializeField] private BallController _ballController;
    [SerializeField] private int winScore; 

    private void Awake()
    {
        Time.timeScale = 0;
        roomMenu.enabled = true;
        gameOverMenu.enabled = false;
        pauseMenu.enabled = false;
        
        leftScore.text = "0";
        rightScore.text = "0";

        currentRoom.text = null;

    }
    

    public void OpenRoomsMenu()
    {
        gameOverMenu.enabled = false;
        pauseMenu.enabled = false;
        
        if (roomMenu.isActiveAndEnabled)
        {
            roomMenu.enabled = false;
            Time.timeScale = 1f;
        }
                
        else
        {
            roomMenu.enabled = true;
            Time.timeScale = 0f;
        }
    }
    public void UpdateRoomName(string roomName)
    {
        currentRoom.text = roomName;
        roomMenu.enabled = false; 
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        gameOverMenu.enabled = false;
        pauseMenu.enabled = false;
        Time.timeScale = 1f;
        _ballController.Restart();
    }


    public void RecolorScores()
    {
        if (_avatar.IsMe && _avatar.gameObject.transform.position.x > 0)
        {
            leftLabel.text = "Them";
            leftLabel.color = Color.red;
            leftScore.color = Color.red; 
            
            rightLabel.text = "You"; 
            rightLabel.color = Color.green;
            rightScore.color = Color.green; 
        }

        else
        {
            leftLabel.text = "You";
            leftLabel.color = Color.green;
            leftScore.color = Color.green; 
            
            rightLabel.text = "Them"; 
            rightLabel.color = Color.red;
            rightScore.color = Color.red; 
        }
    }
    
#region Base Functions
    public void Pause()
    {
        if (pauseMenu.isActiveAndEnabled)
        {
            pauseMenu.enabled = false;
            Time.timeScale = 1f;
           pauseButton.text = "Pause";
        
        }
                
        else
        {
            pauseMenu.enabled = true;
            Time.timeScale = 0f;
            pauseButton.text = "Resume";
        }
    }
            
    public void GameOver()
    {
        gameOverMenu.enabled = true;
        Time.timeScale = 0;
                
    }
    public void Quit()
    {
        Application.Quit(); 
    }
#endregion

    private void Start()
    {

        if (_avatar == null)
        {
            var player = GameObject.FindGameObjectWithTag("Player").GetComponent<Alteruna.Avatar>();
            if (player.IsMe) _avatar = player; 
        }

        
    }

    private void Update()
    {
        
    }
}

