using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Linq;
using System;

public class PlayerChatManager : NetworkBehaviour
{
    public ScriptableMessages messages;

    private Transform parent;
    private InputField messageText;

    private void Start()
    {
        parent = GameObject.Find("Content Messages").transform;
#pragma warning disable CS0618 // Type or member is obsolete
        messageText = GameObject.Find("InputField Message").GetComponent<InputField>();
#pragma warning restore CS0618 // Type or member is obsolete
        GameObject.Find("Button Send").GetComponent<Button>().onClick.AddListener(SendMessage);
    }

    private void Update()
    {
        if (!isLocalPlayer) return;
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!string.IsNullOrEmpty(messageText.text))
                SendMessage();
        }
    }

    public void SendMessage()
    {
        if (!isLocalPlayer) return;

        Debug.Log("@ Send Message");

        CmdSendMessage(messageText.text);
        messageText.text = "";

        if (Application.platform == RuntimePlatform.WindowsEditor)
            messageText.ActivateInputField();
    }

    public void SendSystemMessage(string text, MessageType messageType)
    {
        if (!isLocalPlayer) return;

        Debug.Log("@ Send Message");

        CmdSendSystemMessage(messageText.text, messageType);
        messageText.ActivateInputField();
    }

    [Command]
    private void CmdSendMessage(string text)
    {
        Debug.Log("@ CMD Send Message");
        RpcReadText(text);
    }

    [Command]
    private void CmdSendSystemMessage(string text, MessageType messageType)
    {
        RpcReadSystemText(text, messageType);
    }

    [ClientRpc]
    private void RpcReadText(string text)
    {
        Debug.Log("@ RPC Send Message");

        GameObject prefab = (isLocalPlayer) ? GetMessage(MessageType.Green).messagePrefab : GetMessage(MessageType.Blue).messagePrefab;
        Transform newMessage = Instantiate(prefab, parent).transform.GetChild(0);

        Text textMessage = newMessage.Find("Text Message").GetComponent<Text>();
        Text textDate = newMessage.Find("Text Date").GetComponent<Text>();

        textMessage.text = text;
        textDate.text = string.Format("{0:00}:{1:00}", DateTime.Now.Hour, DateTime.Now.Minute);

        // Scroll to bottom:
        GameObject.Find("Scroll View").GetComponent<ScrollRect>().normalizedPosition = Vector2.zero;

    }

    [ClientRpc]
    private void RpcReadSystemText(string text, MessageType messageType)
    {
        Debug.Log("@ RPC Send Message");

        GameObject prefab = GetMessage(messageType).messagePrefab;
        Transform newMessage = Instantiate(prefab, parent).transform.GetChild(0);
        Text textMessage = newMessage.Find("Text Message").GetComponent<Text>();
        textMessage.text = text;
    }

    private Message GetMessage(MessageType messageType)
    {
        return messages.messages.FirstOrDefault(x => x.messageType.Equals(messageType));
    }
}