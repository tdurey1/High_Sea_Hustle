using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

/*
    Source: https://sharpcoderblog.com/blog/pun-2-adding-room-chat
*/

public class Chat : MonoBehaviourPun
{
    bool isChatting = false;
    string chatInput = "";
    public GUISkin customSkin;
    Vector2 currentScrollPos = new Vector2();

    [System.Serializable]
    public class ChatMessage
    {
        public string sender = "";
        public string message = "";
        public float timer = 0;
    }

    List<ChatMessage> chatMessages = new List<ChatMessage>();

    // Start is called before the first frame update
    void Start()
    {
        //Initialize Photon View
        if (gameObject.GetComponent<PhotonView>() == null)
        {
            PhotonView photonView = gameObject.AddComponent<PhotonView>();
            photonView.ViewID = 11;
        }
        else
        {
            photonView.ViewID = 11;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.T) && !isChatting)
        {
            isChatting = true;
            chatInput = "";
        }

        //Hide messages after timer is expired
        for (int i = 0; i < chatMessages.Count; i++)
        {
            if (chatMessages[i].timer > 0)
            {
                chatMessages[i].timer -= Time.deltaTime;
            }
        }
    }

    void OnGUI()
    {
        GUI.skin = customSkin;

        //if (!isChatting)
        //{
        //    GUI.Label(new Rect(5, Screen.height - 27, 200, 25), "Press 'T' to chat");
        //}
        //else
        //{
        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return)
        {
            isChatting = true;
            if (chatInput.Replace(" ", "") != "")
            {
                //Send message
                photonView.RPC("SendChat", RpcTarget.All, GameInfo.username, chatInput);
            }
            chatInput = "";
        }

        GUILayout.BeginVertical();

        //Show messages
        for (int i = 0; i < chatMessages.Count; i++)
        {
            GUILayout.BeginHorizontal();
            if (chatMessages[i].timer > 0 || isChatting)
            {
                GUI.Label(new Rect(5, Screen.height - 75 - 40 * i, 250, 50), chatMessages[i].sender + ": " + chatMessages[i].message);
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(3);
        }
        GUILayout.EndVertical();

        GUI.SetNextControlName("ChatField");
        GUIStyle inputStyle = GUI.skin.GetStyle("box");
        inputStyle.alignment = TextAnchor.MiddleLeft;
        chatInput = GUI.TextField(new Rect(5, Screen.height - 27, 250, 30), chatInput, 60);

        GUI.FocusControl("ChatField");
        //}
    }

    [PunRPC]
    void SendChat(string username, string message)
    {
        ChatMessage m = new ChatMessage();
        m.sender = username;
        m.message = message;
        m.timer = 15.0f;

        chatMessages.Insert(0, m);
        if (chatMessages.Count > 8)
        {
            chatMessages.RemoveAt(chatMessages.Count - 1);
        }
    }
}