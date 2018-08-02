using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using System.Linq;

[RequireComponent(typeof(NetworkManagerHUD))]
public class CustomNetworkLobbyManager : NetworkLobbyManager
{
    private void Start()
    {
        if (!Application.isEditor && minPlayers != 2) minPlayers = 2;
    }

    public void btn_Connect()
    {
        _Start();
        _ListMatches();
    }
    
    public void btn_DisconnectLobby()
    {
        _Start();
        _DisconnectLobby();
    }

    public void btn_DisconnectServer()
    {
        _Start();
        _DisconnectServer();
    }

    private void _Start()
    {
        if(matchMaker ==  null)
        {
            this.StartMatchMaker();
        }
    }

    private void _ListMatches()
    {
        Debug.Log("@ List matches");
        this.matchMaker.ListMatches(0, 1000, "", true, 0, 0, OnMatchList);
    }

    private void _DisconnectLobby()
    {
        Debug.Log("@ Disconnect from lobby");
        NetworkManager  manager = GetComponent<NetworkManagerHUD>().manager;

        if (manager.IsClientConnected())
        {
            if (NetworkServer.active) manager.StopHost();
            else manager.StopClient();
        }

        EnableLobby(false);
    }

    private void _DisconnectServer()
    {
        Debug.Log("@ Disconnect from server");
        NetworkManager manager = GetComponent<NetworkManagerHUD>().manager;

        if (manager.IsClientConnected())
        {
            if (NetworkServer.active)
            {
                manager.StopHost();
            }
            else
            {
                manager.StopClient();
            }
        }
    }

    private void _JoinMatch(MatchInfoSnapshot match)
    {
        Debug.Log("@ On Join match: " + match.networkId);
        this.matchMaker.JoinMatch(match.networkId, "", "", "", 0, 0, OnMatchJoined);
    }

    private void _CreateMatch()
    {
        Debug.Log("@ Create match");
        this.matchMaker.CreateMatch("Chat", 2, true, "", "", "", 0, 0, OnMatchCreate);
    }

    public override void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList)
    {
        base.OnMatchList(success, extendedInfo, matchList);

        if (!success)
        {
            Debug.LogError("@ List failed: " + extendedInfo);
        }
        else if(matchList.Count > 0)
        {
            Debug.Log("@ Successful listed matches. 1st match: " + matchList[0]);
            _JoinMatch(matchList[0]);
        }
        else
        {
            _CreateMatch();
        }
    }

    public override void OnMatchJoined(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        base.OnMatchJoined(success, extendedInfo, matchInfo);

        if (!success)
        {
            Debug.LogError("@ Failed to join match: " + extendedInfo);
        }
        else
        {
            Debug.Log("@ Succesful joined to match: " + matchInfo.networkId);
            EnableLobby(true);
        }
    }

    public override void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        base.OnMatchCreate(success, extendedInfo, matchInfo);

        if (!success)
        {
            Debug.LogError("@ Failed to create match: " + extendedInfo);
        }
        else
        {
            Debug.Log("@ Successful created match: " + matchInfo.networkId);
            EnableLobby(true);
        }
    }

    private void EnableLobby(bool isEnable)
    {
        CanvasGroup canvasGroup = GameObject.Find("Panel Lobby").GetComponent<CanvasGroup>();
        canvasGroup.alpha = isEnable ? 1 : 0;
        canvasGroup.blocksRaycasts = isEnable;
    }
}
