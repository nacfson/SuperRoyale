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
        player = PhotonNetwork.Instantiate("DefaultPlayer", new Vector3(0,10f,0f), Quaternion.identity).GetComponent<PlayerController>();

        //player.Init();
        //can await

        CameraManager.Instance.SetFollowTarget(player.transform);
        var component = UIManager.Instance.GetUIComponent<InGameUI>();
        UIManager.Instance.AppearUI(component);

        Invoke(nameof(GameStart), 1f);
    }

    public void GameStart()
    {
        PlayerController[] controllers =FindObjectsOfType<PlayerController>();
        foreach(PlayerController controller in controllers)
        {
            controller.Init();
        }
    }
}

