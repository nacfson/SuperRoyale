using System;
using UnityEngine;
using Photon.Realtime;
using TMPro;
using Photon.Pun;

public class RoomUI : UIComponent
{
    [SerializeField] private Transform _contentTrm;

    public override void Appear(Action Callback = null)
    {
        this.gameObject.SetActive(true);
    }

    public override void Disappear(Action Callback = null)
    {
        this.gameObject.SetActive(false);
    }

    public override void Init(params object[] param)
    {
        NetworkManager.Instance.OnPlayerJoinedEvent += CreatePlayerUI;
        NetworkManager.Instance.OnJoinedRoomEvent += CreatePlayerUI;
    }

    private void CreatePlayerUI(Player newPlayer)
    {
        //기존 텍스트 삭제
        if(_contentTrm.childCount > 0)
        {
            for(int i = 0; i <  _contentTrm.childCount; i++)
            {
                if (_contentTrm.GetChild(i).TryGetComponent(out RoomText text))
                    PoolManager.Instance.Push(text);
            }
        }

        //새로운 텍스트 생성
        Player[] players = PhotonNetwork.PlayerList;

        Debug.Log($"PlayerCount: {players.Length}");
        foreach (Player player in players)
        {
            RoomText roomText = PoolManager.Instance.Pop("RoomText") as RoomText;

            roomText.SetText(player.UserId.ToString());
            roomText.transform.SetParent(_contentTrm);
        }

    }
}
