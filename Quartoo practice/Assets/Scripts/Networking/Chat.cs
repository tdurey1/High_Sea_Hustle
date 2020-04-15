using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

/*
    Source: https://sharpcoderblog.com/blog/pun-2-adding-room-chat
*/

public class Chat : MonoBehaviourPun
{
    bool isChatting = false;
    string chatInput = "";
    public GUISkin customSkin;
    public Button startChat;
    public Text chatBtnText;
    private string clickToChat = "Click to chat";
    private string clickToClose = "Click to close chat";
    Vector3 btnPosition = new Vector3();
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
        btnPosition = startChat.transform.position;
        Button btn = startChat.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void OnGUI()
    {
        GUI.skin = customSkin;

        if (isChatting)
        //{
        //    GUI.Label(new Rect(5, Screen.height - 27, 200, 25), "Press 'T' to chat");
        //}
        //else
        {
        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return)
        {
            //isChatting = false;
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
                GUI.Label(new Rect(5, Screen.height - 86 - 40 * i, 250, 50), chatMessages[i].sender + ": " + chatMessages[i].message);
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(3);
        }
        GUILayout.EndVertical();

        GUI.SetNextControlName("ChatField");
        GUIStyle inputStyle = GUI.skin.GetStyle("box");
        inputStyle.alignment = TextAnchor.MiddleLeft;
        chatInput = GUI.TextField(new Rect(10, Screen.height - 35, 270, 30), chatInput, 50);

        GUI.FocusControl("ChatField");
        }
    }

    void TaskOnClick()
    {
        btnPosition.x = (isChatting) ? btnPosition.x - 70f : btnPosition.x + 70f;
        btnPosition.y = (isChatting) ? btnPosition.y + 4f : btnPosition.y - 4f;
        startChat.transform.position = btnPosition;
        isChatting = (isChatting) ? false : true;
        chatBtnText.text = (chatBtnText.text == clickToChat) ? clickToClose : clickToChat;
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

        if (!isChatting)
            TaskOnClick();
    }
}