using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;


public class RoomButton : MonoBehaviour
{
    public Canvas RoomLobbyCanvas;
    public Canvas WaitingLoadingCanvas;

    public Text nameText;
    public string roomName;

    public void SetRoom()
    {
        nameText.text = roomName;
    }

    public void JoinRoomOnClick()
    {
        Debug.Log("F: RoomButton.cs/public void JoinRoomOnClick - Room we want to join was clicked");
        PhotonNetwork.JoinRoom(roomName);
        Debug.Log("F: RoomButton.cs/public void JoinRoomOnClick - Succesfully joined room: " + roomName);
        RoomLobbyCanvas.gameObject.SetActive(false);
        WaitingLoadingCanvas.gameObject.SetActive(true);
    }
}
