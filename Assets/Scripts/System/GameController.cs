using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private void Start()
    {
        PlayerController player = PhotonNetwork.Instantiate("DefaultPlayer", Vector3.zero, Quaternion.identity).GetComponent<PlayerController>();

        CameraManager.Instance.SetFollowTarget(player.transform);
    }


    private Vector3 GetSpawnPoint()
    {
        return Vector3.zero;
    }
}
