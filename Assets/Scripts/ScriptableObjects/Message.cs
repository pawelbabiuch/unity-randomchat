using UnityEngine;

[System.Serializable]
public class Message
{
    public MessageType messageType;
    public GameObject messagePrefab;
}

public enum MessageType
{
    Info, System, Green, Blue
}
