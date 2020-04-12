using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartRoom : MonoBehaviourPunCallbacks, ILobbyCallbacks
{
    #region Variables

    public static StartRoom room;

    public GameObject CreateGameButton;
    public GameObject JoinGameButton;
    public GameObject BackButton;
    public GameObject FindGamesButton;
    public GameObject roomListingPrefab;
    public GameObject roomListingPanel;
    public GameObject StartButton;

    public Canvas CreateOrJoinCanvas;
    public Canvas RoomLobbyCanvas;
    public Canvas WaitingLoadingCanvas;
    public Canvas LoadingCanvas;

    public Text StatusText;
    
    public Transform roomsPanel;

    public string roomName;

    #endregion

    #region AwakeStartUpdate

    private void Awake()
    {
        room = this;
    }

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();   // -> OnConnectedToMaster
    }

    #endregion

    #region PunCallbacks

    public override void OnConnectedToMaster()
    {
        CreateOrJoinCanvas.gameObject.SetActive(true);        

        PhotonNetwork.JoinLobby();  // -> OnJoinedLobby
    }

    public override void OnJoinedLobby()
    {
        CreateGameButton.SetActive(true);
        JoinGameButton.SetActive(true);
        BackButton.SetActive(true);
        LoadingCanvas.gameObject.SetActive(false);
    }

    public override void OnCreatedRoom()
    {
        PhotonNetwork.AutomaticallySyncScene = true;    // -> OnJoinedRoom
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
    }

    public override void OnJoinedRoom()
    {
        CreateOrJoinCanvas.gameObject.SetActive(false);
        RoomLobbyCanvas.gameObject.SetActive(false);

        WaitingLoadingCanvas.gameObject.SetActive(true);
        if (!PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            StatusText.text = "Connected to room, waiting for host to start game...";
        }

        StartButton.gameObject.SetActive(false);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
    }    

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        RemoveRoomListings();

        foreach (RoomInfo room in roomList)
        {
            ListRoom(room);
        }
    }   

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            if (PhotonNetwork.LocalPlayer.IsMasterClient)
            {
                StatusText.text = "Player joined, ready to Start Game...";
                StartButton.SetActive(true);
            }
        }
    }    

    public override void OnLeftLobby()
    {
        base.OnLeftLobby();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
    }

    #endregion

    #region Functions

    public void CreateRoom()
    {
        RoomOptions roomOps = new RoomOptions()
        {
            EmptyRoomTtl = 1,
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = 2
        };

        roomName = GameInfo.username;
        PhotonNetwork.CreateRoom(roomName, roomOps);    // -> OnCreatedRoom / OnCreateRoomFailed
        
    }

    public void RemoveRoomListings()
    {
        while (roomsPanel.childCount != 0)
        {
            Destroy(roomsPanel.GetChild(0).gameObject);
        }
    }

    public void ListRoom(RoomInfo room)
    {
        if (room.IsOpen && room.IsVisible)
        {
            GameObject tempListing = Instantiate(roomListingPrefab, roomsPanel);
            RoomButton tempButton = tempListing.GetComponent<RoomButton>();
            tempButton.roomName = room.Name;
            tempButton.SetRoom();
        }
    }


    #endregion

    #region Buttons

    public void OnCreateGameButtonClicked()
    {
        CreateRoom();

        GameInfo.selectPieceAtStart = 1;
    }

    public void OnJoinGameButtonClicked()
    {
        CreateOrJoinCanvas.gameObject.SetActive(false);
        RoomLobbyCanvas.gameObject.SetActive(true);

        FindGamesButton.gameObject.SetActive(true);
        roomListingPanel.gameObject.SetActive(true);
  
        GameInfo.selectPieceAtStart = 2;
    }


    public void OnBackButtonClicked()
    {
        if (CreateOrJoinCanvas.isActiveAndEnabled)
        {
            PhotonNetwork.Disconnect();
            Initiate.Fade("MainMenu", Color.black, 4.0f);
        }

        if (RoomLobbyCanvas.isActiveAndEnabled || WaitingLoadingCanvas.isActiveAndEnabled)
        {
            if (PhotonNetwork.InRoom)
            {                
                PhotonNetwork.LeaveRoom();
            }
            CreateGameButton.SetActive(false);
            JoinGameButton.SetActive(false);
            BackButton.SetActive(false);

            PhotonNetwork.Disconnect();
            CreateOrJoinCanvas.gameObject.SetActive(true);
            LoadingCanvas.gameObject.SetActive(true);
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public void OnStartButtonClicked()
    {
        PhotonNetwork.LoadLevel("GameScene");
    }

    #endregion
}
