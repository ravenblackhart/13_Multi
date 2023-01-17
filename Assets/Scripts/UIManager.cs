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
    [SerializeField] private TMP_Text pauseButton;
    [SerializeField] private Button leaveButton;
        
        
    [Header("RoomMenu")] 
    [SerializeField] private Canvas roomMenu;
    [SerializeField] private Button startButton;
    [SerializeField] private Button joinButton;
    [SerializeField] private TMP_Text titleText; 
     

    [Header("PauseMenu")] 
     
    [SerializeField] private Canvas pauseMenu; 
    
    [Header("GameOverMenu")]
    [SerializeField] private Canvas gameOverMenu;
    
        

    #region Alteruna
    
    

    #endregion



    private void Awake()
    {
        Time.timeScale = 0;
        roomMenu.enabled = true;
        gameOverMenu.enabled = false;
        pauseMenu.enabled = false;
        
        playerScore.text = "0";
        opponentScore.text = "0"; 
        
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

#region Alteruna Functions
     private void Connected(Multiplayer multiplayer, Endpoint endpoint)
    {
        if (titleText != null)
        {
            titleText.text = "Rooms";
        }

        // if (isActiveAndEnabled)
        // {
        //     StartCoroutine(nameof(RefreshRooms));
        // }
    }

    private void Disconnected(Multiplayer multiplayer, Endpoint endPoint)
    {
        if (titleText != null)
        {
            titleText.text = "Reconnecting..";
        }
    }

    // private void UpdateList(Multiplayer multiplayer)
    // {
    //     for (int i = 0; i < _roomObjects.Count; i++)
    //     {
    //         Destroy(_roomObjects[i]);
    //     }
    //     _roomObjects.Clear();
    //
    //     if (ContentContainer != null)
    //     {
    //         for (int i = 0; i < multiplayer.AvailableRooms.Count; i++)
    //         {
    //             Room room = multiplayer.AvailableRooms[i];
    //
    //             GameObject entry;
    //             if (room.Local)
    //             {
    //                 entry = Instantiate(WANEntryPrefab, ContentContainer.transform);
    //             }
    //             else
    //             {
    //                 entry = Instantiate(LANEntryPrefab, ContentContainer.transform);
    //             }
    //
    //             entry.SetActive(true);
    //             _roomObjects.Add(entry);
    //
    //             entry.GetComponentInChildren<Text>().text = room.Name;
    //             entry.GetComponentInChildren<Button>().onClick.AddListener(() => { room.Join(); });
    //         }
    //     }
    // }

    private void JoinedRoom(Multiplayer multiplayer, Room room, User user)
    {
        if (titleText != null)
        {
            currentRoom.text = room.Name;
        }
    }

    private void LeftRoom(Multiplayer multiplayer)
    {
        if (titleText != null)
        {
            titleText.text = "Rooms";
        }
    }

    // private void Start()
    // {
    //     if (_aump == null)
    //     {
    //         _aump = FindObjectOfType<Multiplayer>();
    //         if (!_aump)
    //         {
    //             Debug.LogError("Unable to find a active object of type Multiplayer.");
    //         }
    //     }
    //
    //     if (_aump != null)
    //     {
    //         _aump.Connected.AddListener(Connected);
    //         _aump.Disconnected.AddListener(Disconnected);
    //         _aump.RoomListUpdated.AddListener(UpdateList);
    //         _aump.RoomJoined.AddListener(JoinedRoom);
    //         _aump.RoomLeft.AddListener(LeftRoom);
    //         startButton.onClick.AddListener(() => { _aump.JoinOnDemandRoom(); });
    //         leaveButton.onClick.AddListener(() => { _aump.CurrentRoom?.Leave(); });
    //     }
    //
    //     if (titleText != null)
    //     {
    //         titleText.text = "Connecting..";
    //     }
    // }
    //
    // private IEnumerator RefreshRooms()
    // {
    //     while (AutomaticallyRefresh) {
    //         yield return new WaitForSeconds(RefreshInterval);
    //         _aump.RefreshRoomList();
    //     }
    // }
#endregion

}
