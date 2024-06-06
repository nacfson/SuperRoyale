using System;
using UnityEngine.UI;

public class LobbyUI : UIComponent
{
    private Button _joinBtn;
    private Button _createRoomBtn;

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
        _joinBtn = transform.Find("JoinBtn").GetComponent<Button>();
        _createRoomBtn = transform.Find("CreateRoomBtn").GetComponent<Button>();

        _joinBtn.onClick.AddListener(() => NetworkManager.Instance.JoinRoom());
        _createRoomBtn.onClick.AddListener(() => NetworkManager.Instance.CreateRoom());
    }
}
