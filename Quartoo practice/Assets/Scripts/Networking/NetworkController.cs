using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NetworkController : MonoBehaviour
{
    public static NetworkController NetController;

    private PhotonView photonView;
    private static string networkMessage;
    private static bool networkMessageReceived = false;

    // Move variable
    public static string movePiece;
    public static string moveLocation;


    void Awake()
    {
        NetController = this;
    }

    private void Start()
    {
        photonView.GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        // Called to distinguish network game
        if (photonView != null)
        {
            // if my pv is not sending a message
            if (!photonView.IsMine)
                return;
            // check once per frame if a message has been sent
            else
                photonView.RPC("receiveNetworkMessage", RpcTarget.All, networkMessage);
        }
    }

    /*
     * [PunRPC]
     * void functionName(data youWantToPass)
     * {
     *      code to be executed on the receiving end of the rpc
     * }
     * 
     * How to send RPC:
     * photonView.RPC("functionName", RpcTarget.whoYouWantToSendThisTo, valueToBeTransferred);
     */

    public IEnumerator WaitForTurn()
    {
        Debug.Log("Network waiting for turn");
        while (networkMessageReceived == false)
        {
            yield return null;
        }
        networkMessageReceived = false;
    }

    [PunRPC]
    void receiveNetworkMessage(string message)
    {
        Debug.Log("Message sent over network: " + message);
    }

    [PunRPC]
    public void RPC_SendMove(string location, string piece)
    {
        if (photonView.IsMine)
            return;

        Debug.Log("Receiving move...");
        movePiece = piece;
        moveLocation = location;
        networkMessageReceived = true;
    }

    public void SendMove()
    {
        photonView.RPC("RPC_SendMove", RpcTarget.All, moveLocation, movePiece);
    }
}
