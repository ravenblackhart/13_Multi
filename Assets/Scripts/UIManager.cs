using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Alteruna;
using TMPro; 

public class UIManager : MonoBehaviour
{
    [Header ("MainUI")]
    [SerializeField] private TMP_Text currentRoom; 
    [SerializeField] private TMP_Text playerScore;
    [SerializeField] private TMP_Text opponentScore;
    [SerializeField] private TMP_Text roomTitle; 
    [SerializeField] private TMP_Text pauseButton;
    [SerializeField] private Button leaveButton;

    [Header("RoomMenu")]
    [SerializeField] private Canvas roomMenu; 

    [Header("PauseMenu")]
    [SerializeField] private Canvas pauseMenu; 
    
    [Header("GameOverMenu")]
    [SerializeField] private Canvas gameOverMenu;



    private void Awake()
    {
        Time.timeScale = 0;
        roomMenu.enabled = true;
        gameOverMenu.enabled = false;
        pauseMenu.enabled = false;
        
        playerScore.text = "0";
        opponentScore.text = "0"; 
        
    }

    public void UpdateRoomName(string roomName)
    {
        roomTitle.text = roomName;
        roomMenu.enabled = false; 
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


}
