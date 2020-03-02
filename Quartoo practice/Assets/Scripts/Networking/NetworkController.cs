using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NetworkController : MonoBehaviour
{
    public static NetworkController NetController;

    [SerializeField] private PhotonView photonView;
    private static string networkMessage = "";
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
        if (photonView == null)
        {
            Debug.Log("Instantiated PV");
          
            GameObject player = PhotonNetwork.Instantiate("NetworkPlayer", new Vector3(0, 0, 0), Quaternion.identity, 0);
            //  photonView = player.GetComponent<PhotonView>();
            photonView = PhotonView.Get(this);
            Debug.Log(photonView);
        }
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
            //else
                //photonView.RPC("RPC_receiveNetworkMessage", RpcTarget.All, networkMessage);
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


    // if it is not our turn, we're waiting for a message
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
    public void RPC_receiveNetworkMessage(string message)
    {
        Debug.Log("Message sent over network: " + message);
    }

    [PunRPC]
    public void RPC_SendMove(string location, string piece)
    {
        Debug.Log(photonView);
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

    public void SetMovePiece(string id)
    {
        movePiece = id;
    }

    public void SetMoveLocation(string name)
    {
        moveLocation = name;
    }
}
