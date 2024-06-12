using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;

    private void Start()
    {
        PlayerController player;
        try
        {
            player = PhotonNetwork.Instantiate("DefaultPlayer", new Vector3(0,10f,0f), Quaternion.identity).GetComponent<PlayerController>();

        }
        catch
        {
            player = Instantiate(_playerController);
        }


        CameraManager.Instance.SetFollowTarget(player.transform);
        var component = UIManager.Instance.GetUIComponent<InGameUI>();
        UIManager.Instance.AppearUI(component);
    }
}
