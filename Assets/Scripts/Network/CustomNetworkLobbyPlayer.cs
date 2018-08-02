using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(NetworkIdentity))]
public class CustomNetworkLobbyPlayer : NetworkLobbyPlayer
{
    public override void OnStartLocalPlayer()
    {
        Debug.Log("@ On Start Local Player");
        base.OnStartLocalPlayer();
        SendReadyToBeginMessage();
    }
}
