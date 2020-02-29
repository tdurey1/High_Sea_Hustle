using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartRoom : MonoBehaviourPunCallbacks, ILobbyCallbacks
{
    #region Variables

    public static StartRoom room;

    public GameObject CreateGameButton;
    public GameObject JoinGameButton;
    public GameObject BackButton;
    public GameObject StartGameButton;
    public GameObject FindGamesButton;
    public GameObject roomListingPrefab;
    

    public Transform roomsPanel;

    public string roomName;
    private string currentCanvas;

    #endregion

    #region AwakeStartUpdate

    private void Awake()
    {
        room = this;
    }

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("F: StartRoom.cs/private void Start - Connected to photon servers");
        currentCanvas = "CreateOrJoinCanvas";
    }

    #endregion

    #region PunCallbacks

    public override void OnConnectedToMaster()
    {
        Debug.Log("F: StartRoom.cs/public override void OnConnectedToMaster - Connected to photon master server");
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("F: StartRoom.cs/public ovveride void OnRoomListUpdate - Change in the rooms available");
        base.OnRoomListUpdate(roomList);
        RemoveRoomListings();

        foreach (RoomInfo room in roomList)
        {
            ListRoom(room);
        }
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("F: StartRoom.cs/public override void OnCreateRoomFailed - Room with same name exists");

        // Users with the same name???
    }

    #endregion

    #region Functions

    public void CreateRoom()
    {
        Debug.Log("F: StartRoom.cs/void CreateRoom - Creating new room");
        RoomOptions roomOps = new RoomOptions()
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = 2
        };

        // Room name needs to be player name???
        roomName = Random.Range(0, 10000).ToString();
        PhotonNetwork.CreateRoom(roomName, roomOps);
    }

    public void RemoveRoomListings()
    {
        Debug.Log("F: StartRoom.cs/void RemoveRoomListing");
        while (roomsPanel.childCount != 0)
        {
            Destroy(roomsPanel.GetChild(0).gameObject);
        }
    }

    public void ListRoom(RoomInfo room)
    {
        Debug.Log("F: StartRoom.cs/void ListRoom");
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
        Debug.Log("Create button clicked");
        CreateRoom();
        currentCanvas = "RoomLobbyCanvas";
    }

    public void OnJoinGameButtonClicked()
    {
        currentCanvas = "RoomLobbyCanvas";
    }

    public void OnCancelButtonClicked()
    {
        Debug.Log("F: StartRoom.cs/public void OnCancelButtonClicked - Cancel button was clicked");
        // PhotonNetwork.LeaveRoom();
    }

    public void JoinLobbyOnClick()
    {
        Debug.Log("F: StartRoom.cs/public void JoinLobbyOnClick");
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    public void OnBackButtonClicked()
    {
        if (currentCanvas == "CreateOrJoinCanvas")
        {
            PhotonNetwork.Disconnect();
            SceneManager.LoadScene("MainMenu");
        }

        if (currentCanvas == "RoomLobbyCanvas" || currentCanvas == "WaitingLoadingCanvas")
        {
            if (PhotonNetwork.InRoom)
            {
                PhotonNetwork.LeaveRoom();
            }
            currentCanvas = "CreateOrJoinCanvas";
        }
    }

    #endregion
}
