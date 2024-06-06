using UnityEngine.UI;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System;

public class TitleUI : UIComponent
{
    private Dictionary<string, UIComponent> _componentDictionary;
    private TextMeshProUGUI _stateText;

    public override void Init(params object[] param)
    {
        _componentDictionary = new Dictionary<string, UIComponent>();
        for(int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).TryGetComponent(out UIComponent component))
            {
                _componentDictionary.Add(transform.GetChild(i).name,component);
                component.Init(param);
            }
        }

        _stateText = transform.Find("StateText").GetComponent<TextMeshProUGUI>();

        if(NetworkManager.Instance == null)
        {
            Debug.LogError("NetworkManager is null!");
            return;
        }

        NetworkManager.Instance.OnNetworkStateChangeEvent += UpdateStateText;
        NetworkManager.Instance.OnJoinedLobbyEvent += JoinedLobby;
        NetworkManager.Instance.OnJoinedRoomEvent += JoinedRoom;
    }

    private void UpdateStateText(string text) => _stateText.SetText(text);

    private void JoinedLobby()
    {
        _componentDictionary["Room"].Appear();
        _componentDictionary["Lobby"].Disappear();
    }

    private void JoinedRoom()
    {
        _componentDictionary["Room"].Disappear();
        _componentDictionary["Lobby"].Appear();
    }

    public override void Appear(Action Callback = null)
    {
    }

    public override void Disappear(Action Callback = null)
    {
    }
}
