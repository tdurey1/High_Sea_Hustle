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


    [PunRPC]
    public void RPC_SendMove(string location, string piece)
    {
        Debug.Log("F: NetworkPlayer.cs/RPC_SendMove was called");
        if (photonView.IsMine)
            return;

        Debug.Log("Receiving move...");
        movePiece = piece;
        moveLocation = location;
        networkMessageReceived = true;
    }

    //[PunRPC]
    //public void RPC_SendMove(string location, string piece)
    //{
    //    Debug.Log(photonView);
    //    if (photonView.IsMine)
    //        return;

    //    Debug.Log("Receiving move...");
    //    movePiece = piece;
    //    moveLocation = location;
    //    networkMessageReceived = true;
    //}
}
