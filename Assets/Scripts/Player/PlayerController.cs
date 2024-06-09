using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PhotonView _PV;
    public InputReader InputReader {get;private set; }
    public CharacterController Controller {get; private set; }


    private StateMachine<PlayerController> _stateMachine;
    private List<PlayerModule> _moduleList;
    public bool IsMine => _PV.IsMine;
    
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _stateMachine = new StateMachine<PlayerController>();
        InputReader = GameManager.Instance.InputReader;
        _PV = GetComponent<PhotonView>();
        Controller = GetComponent<CharacterController>();

        _moduleList = new List<PlayerModule>();
        GetComponentsInChildren(_moduleList);
        _moduleList.ForEach(module => module.Init(this));
    }

    private void Update()
    {
        if (!IsMine) return;

    }

    private void FixedUpdate()
    {
        if (!IsMine) return;


    }

    public M GetPlayerModule<M>() where M : PlayerModule
    {
        foreach(PlayerModule module in _moduleList)
        {
            if(module is M findModule)
            {
                return findModule;
            }
        }
        Debug.LogError($"Can't Find module in moduleList: {_moduleList.Count}");
        return null;
    }
}

