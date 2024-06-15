using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using Random = UnityEngine.Random;
using Photon.Pun.Demo.Cockpit;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public delegate void NetworkEventListener();
    public delegate void NetworkEventListener<T>(T value);

    public event NetworkEventListener<string> OnNetworkStateChangeEvent;
    public event NetworkEventListener OnJoinedLobbyEvent;

    public event NetworkEventListener<Player> OnJoinedRoomEvent;
    public event NetworkEventListener<Player> OnPlayerJoinedEvent;

    public event NetworkEventListener OnPlayerLeftEvent;
    public event NetworkEventListener OnGameStartEvent;

    public static NetworkManager Instance;

    public PhotonView PV {get; private set; }


    public int ReadyCount { get; private set; }
    public int PlayerCount { get; private set; }

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        PV = GetComponent<PhotonView>();
        PhotonNetwork.ConnectUsingSettings();
    }

    #region MyMethod

    [PunRPC]
    public void ShootingEvent_RPC(int eventType, params object[] param)
    {
        var gameEvent = Events.GetEvent((EventType)eventType);
        gameEvent.Setting(param);

        EventManager.Broadcast(gameEvent);
    }

    [PunRPC]
    public void NetworkCreate_RPC(string name,Vector3 pos)
    {
        PoolableMono obj = PoolManager.Instance.Pop(name);
        ObjectManager.Instance.AddMono(obj);
        obj.transform.position = pos;
    }

    [PunRPC]
    public void ReadyPlayer_RPC(int actorNumber, bool isReady)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if(isReady)
            {
                ReadyCount++;
            }
            else
            {
                ReadyCount--;
            }

            ReadyCheck();
        }
    }

    [PunRPC]
    public void StartGame_RPC()
    {
        OnGameStartEvent?.Invoke();
    }

    public void CreateEvent(RpcTarget target, EventType type, params object[] param)
    {
        RPCShooter(nameof(ShootingEvent_RPC), RpcTarget.All, (int)type, param);
    }


    public bool ReadyCheck()
    {
        if (ReadyCount == PlayerCount)
        {
            RPCShooter(nameof(StartGame_RPC), RpcTarget.All);
            return true;
        }
        else if (ReadyCount > PlayerCount)
        {
            Debug.LogError("Ready is multiplied!");
        }
        return false;
    }

    public void RPCShooter(string methodName,RpcTarget target, params object[] param)
    {
        PV.RPC(methodName, target, param);
    }

    public void CreateRoom()
    {
        string roomName = Random.Range(0000, 9999).ToString();
        RoomOptions options = new RoomOptions();
        options.PublishUserId = true;

        PhotonNetwork.CreateRoom(roomName,options);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRandomRoom();
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
        PhotonNetwork.AutomaticallySyncScene = true;

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
        PlayerCount = PhotonNetwork.PlayerList.Count();
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
        OnPlayerLeftEvent?.Invoke();
        PlayerCount--;
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
        PlayerCount++;
    }
    #endregion 
}