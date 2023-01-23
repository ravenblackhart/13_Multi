using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;
using Alteruna.Trinity;
using TMPro;
using UnityEngine.UIElements;
using Application = UnityEngine.Application;
using Button = UnityEngine.UI.Button;


public class UIManager : AttributesSync
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
    [SerializeField] private TMP_Text gameOverTextField; 
    
    
    [Header("Misc")]
    private Alteruna.Avatar _avatar;
    [SerializeField] private BallController _ballController;
    [SerializeField] private int winScore;

    private List<WallController> _wallControllers;
    private List<PlayerController2D> _playerControllers; 
    private int leftInt;
    private int rightInt;

    private void GameOverRPC(ushort fromUser, ProcedureParameters parameters, uint callId, ITransportStreamReader processor) {
        GameOver(parameters.Get("text", String.Empty), parameters.Get("useLeftColor", false) ? leftLabel.color : rightLabel.color);
    }

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

    private void Start()
    {
        _wallControllers = new List<WallController>(); 
        _wallControllers.AddRange(FindObjectsOfType<WallController>());

        _playerControllers = new List<PlayerController2D>(); 
        _playerControllers.AddRange(FindObjectsOfType<PlayerController2D>());
        
        Multiplayer.RegisterRemoteProcedure("GameOverRPC", GameOverRPC);
    }


    public void OpenRoomsMenu()
    {
        Multiplayer.RefreshRoomList();
        gameOverMenu.enabled = false;
        pauseMenu.enabled = false;
        
        
        if (roomMenu.isActiveAndEnabled)
        {
            roomMenu.enabled = false;
            Time.timeScale = 1f;
            foreach (var item in _playerControllers) item.enabled = true; 
        }
                
        else
        {
            roomMenu.enabled = true;
            Time.timeScale = 0f;
            foreach (var item in _playerControllers) item.enabled = false; 
            
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
        BroadcastRemoteMethod("RestartBroadcast");
    }

    [SynchronizableMethod]
    void RestartBroadcast()
    {
        foreach (var item in _wallControllers) item.ResetPoints();
        
        gameOverMenu.enabled = false;
        pauseMenu.enabled = false;

        leftInt = 0;
        rightInt = 0;

        Time.timeScale = 1f;
        _ballController.RestartBallPosition();
    }
    
    public void RecolorScores(Alteruna.Avatar player)
    {

        if (player.transform.position.x > 0)
        {
            leftLabel.text = "Opponent";
            leftLabel.color = Color.red;
            leftScore.color = Color.red; 
            
            rightLabel.text = "You"; 
            rightLabel.color = Color.green;
            rightScore.color = Color.green; 
            
            Debug.Log("You are right");
        }
        else
        {
            leftLabel.text = "You";
            leftLabel.color = Color.green;
            leftScore.color = Color.green; 
            
            rightLabel.text = "Opponent"; 
            rightLabel.color = Color.red;
            rightScore.color = Color.red;
        }
    }
    
    public void TriggerGameOver()
    {
        leftInt = Convert.ToInt32(leftScore.text) + 1; 
        rightInt = Convert.ToInt32(rightScore.text) + 1;

        if (leftInt >= winScore) {
            ProcedureParameters parameters = new ProcedureParameters();
            parameters.Set("text", rightLabel.text + " Won !");
            parameters.Set("useLeftColor", true);

            Multiplayer.InvokeRemoteProcedure("GameOverRPC", UserId.All, parameters);   
            GameOver(leftLabel.text + " Won !", leftLabel.color);
        }
        else if (rightInt >= winScore) 
        {
            ProcedureParameters parameters = new ProcedureParameters();
            parameters.Set("text", leftLabel.text + " Won !");
            parameters.Set("useLeftColor", false);
            
            Multiplayer.InvokeRemoteProcedure("GameOverRPC", UserId.All, parameters);
            GameOver(rightLabel.text + " Won !", rightLabel.color);
        }
    }

    #region Base Functions
    public void Pause()
    {
        BroadcastRemoteMethod("PauseBraodcast");
    }

    [SynchronizableMethod]
    void PauseBraodcast()
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
            
    public void GameOver(string gameOverText, Color color)
    {
        gameOverTextField.text = gameOverText;
        gameOverTextField.color = color;
        gameOverMenu.enabled = true;

        Time.timeScale = 0;
    }
    public void Quit()
    {
        Application.Quit(); 
    }
#endregion

}

