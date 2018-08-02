using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AddListenerToButton : MonoBehaviour
{
    [SerializeField]
    private Functions functionName;

    private void Start()
    {
        CustomNetworkLobbyManager networkSystem = GameObject.Find("_NetworkSystem").GetComponent<CustomNetworkLobbyManager>();

        switch (functionName)
        {
            case Functions.Connect:
                GetComponent<Button>().onClick.AddListener(networkSystem.btn_Connect);
                break;
            case Functions.DisconnectLobby:
                GetComponent<Button>().onClick.AddListener(networkSystem.btn_DisconnectLobby);
                break;
            case Functions.DisconnectServer:
                GetComponent<Button>().onClick.AddListener(networkSystem.btn_DisconnectServer);
                break;
        }
    }
}

public enum Functions
{
    Connect, DisconnectLobby, DisconnectServer
}
