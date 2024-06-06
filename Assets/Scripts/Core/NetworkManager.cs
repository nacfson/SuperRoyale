using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using Random = UnityEngine.Random;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public delegate void NetworkEventListener();
    public delegate void NetworkEventListener<T>(T value);

    public event NetworkEventListener<string> OnNetworkStateChangeEvent;
    public event NetworkEventListener OnJoinedLobbyEvent;
    public event NetworkEventListener<Player> OnJoinedRoomEvent;
    public event NetworkEventListener<Player> OnPlayerJoinedEvent;
    public event NetworkEventListener<bool> OnPlayerReadyEvent;

    public static NetworkManager Instance;

    private PhotonView _PV;

    private bool _isReady;
    public bool IsReady
    {
        get => _isReady;
        set
        {
            _isReady = value;
            _PV.RPC("ReadyGame_RPC",RpcTarget.MasterClient);
        }
    }

    private int _readyPlayerCount;


    private void Awake()
    {
        Instance = this;

        _PV = GetComponent<PhotonView>();
        Debug.Log("Connecting To Master");
        PhotonNetwork.ConnectUsingSettings();
    }

    #region MyMethod
    public void CreateRoom()
    {
        string roomName = Random.Range(0000, 9999).ToString();
        RoomOptions options = new RoomOptions();
        options.PublishUserId = true;

        PhotonNetwork.CreateRoom(roomName,options);
    }

    public void ReadyGame()
    {
        _isReady = !_isReady;
        _PV.RPC("ReadyGame_RPC",RpcTarget.MasterClient);
    }

    [PunRPC]
    public void ReadyGame_RPC()
    {
        _readyPlayerCount++;
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }


    public void StartGame()
    {

    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    #endregion

    #region PhotonMethod

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        PhotonNetwork.JoinLobby(TypedLobby.Default);

        OnNetworkStateChangeEvent?.Invoke("OnConnectedToMaster");
    }

    public override void OnJoinedLobby()
    {
        OnJoinedLobbyEvent?.Invoke();
        OnNetworkStateChangeEvent?.Invoke("OnJoinedLobby");
    }

    //JoinRoom 했을 때 플레이어 이름 뜨도록 만들어야함
    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
        OnJoinedRoomEvent?.Invoke(PhotonNetwork.LocalPlayer);
        OnNetworkStateChangeEvent?.Invoke("OnJoinedRoom");

        _isReady = false;
        OnPlayerReadyEvent?.Invoke(_isReady);
    }


    public override void OnMasterClientSwitched(Player newMasterClient)
    {

    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        string text = $"Room Creation Failed {message}";
        OnNetworkStateChangeEvent?.Invoke("OnCreateRoomFailed");
    }


    public override void OnLeftRoom()
    {

    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"Player is joined: {newPlayer}");

        if(newPlayer != null)
        {
            OnPlayerJoinedEvent?.Invoke(newPlayer);
        }

        _isReady = false;
        OnPlayerReadyEvent?.Invoke(_isReady);
    }
    #endregion 
}