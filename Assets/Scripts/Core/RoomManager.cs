using System.IO;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using System.Collections;

public enum GAME_STATE
{
    MENU = 0, LOADING = 1, INGAME = 2, UI = 3, END = 4, CHAT = 5
}


public class RoomManager : MonoSingleton<RoomManager>, IInstanceable
{
    public GAME_STATE GameState { get; private set; }
    public bool IsReady { get; private set; }

    public Dictionary<int, PlayerController> PlayerDictionary { get; private set; }
    public event Action OnGameStart;


    public void CreateInstance()
    { 
        NetworkManager.Instance.OnPlayerLeftEvent += UnReadyGame;
        NetworkManager.Instance.OnGameStartEvent += StartGame;

        PlayerDictionary = new Dictionary<int, PlayerController>();
    }

    private void StartGame()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            int curSceneIndex = SceneManager.GetActiveScene().buildIndex;
            PhotonNetwork.LoadLevel(curSceneIndex + 1);
        }
    }

    public void ReadyGame()
    {
        IsReady = true;
        int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;


        NetworkManager.Instance.RPCShooter(nameof(NetworkManager.Instance.ReadyPlayer_RPC),
            RpcTarget.All, actorNumber, true);
    }

    public void UnReadyGame()
    {
        IsReady = false;
        int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;


        NetworkManager.Instance.RPCShooter(nameof(NetworkManager.Instance.ReadyPlayer_RPC),
            RpcTarget.All, actorNumber, false);
    }

    public void ChangeGameState(GAME_STATE gameState)
    {
        GameState = gameState;
    }
}