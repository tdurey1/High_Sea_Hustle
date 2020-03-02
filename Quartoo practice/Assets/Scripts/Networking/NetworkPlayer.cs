using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkPlayer : MonoBehaviour
{
    public static NetworkPlayer networkPlayer;
    [SerializeField] private PhotonView photonView;
    private static string networkMessage = "";
    private static bool networkMessageReceived = false;

    public static string movePiece;
    public static string moveLocation;

    private void Start()
    {
        photonView = PhotonView.Get(this);
        networkPlayer = this;
    }

    // need?
    [PunRPC]
    public void RPC_receiveNetworkMessage(string message)
    {
        Debug.Log("Message sent over network: " + message);
    }


    [PunRPC]
    public void RPC_SendMove(string location, string piece)
    {
        Debug.Log("F: NetworkPlayer.cs/RPC_SendMove was called");
        if (photonView.IsMine)
            return;

        Debug.Log("Receiving move...");
        //SetMovePiece(movePiece);
        //SetMoveLocation(moveLocation);
        //movePiece = piece;
        //moveLocation = location;
        networkMessageReceived = true;
        Debug.Log(piece);
        Debug.Log(location);
    }

    public void SendMove(string moveLocation, string movePiece)
    {
        photonView.RPC("RPC_SendMove", RpcTarget.All, moveLocation, movePiece);
    }

 
}
