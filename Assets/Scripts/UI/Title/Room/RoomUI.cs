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
        //���� �ؽ�Ʈ ����
        if(_contentTrm.childCount > 0)
        {
            for(int i = 0; i <  _contentTrm.childCount; i++)
            {
                if (_contentTrm.GetChild(i).TryGetComponent(out RoomText text))
                    PoolManager.Instance.Push(text);
            }
        }

        //���ο� �ؽ�Ʈ ����
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
