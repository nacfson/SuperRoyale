using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public enum EPLAYER_DATA
{
    Default,
    Aim,
}

public class PlayerController : MonoBehaviour
{
    private PhotonView _PV;
    [field:SerializeField] public InputReader InputReader {get;private set; }
    public CharacterController Controller {get; private set; }


    private StateMachine<PlayerController> _stateMachine;
    private List<PlayerModule> _moduleList;


    [SerializeField] private List<PlayerData> _dataList;
    public PlayerData CurrentPlayerData {get;private set; }
    private Dictionary<EPLAYER_DATA, PlayerData> _playerDataDictionary = new Dictionary<EPLAYER_DATA, PlayerData>();

    public bool IsMine
    {
        get
        {
            if (_PV == null) return false;
            return _PV.IsMine;
        }
    }
    
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        InputReader = GameManager.Instance.InputReader;
        _PV = GetComponent<PhotonView>();
        Controller = GetComponent<CharacterController>();

        _moduleList = new List<PlayerModule>();

        GetComponentsInChildren(_moduleList);
        _moduleList.ForEach(module => module.Init(this));

        _stateMachine = new StateMachine<PlayerController>(this);

        ChangePlayerData(EPLAYER_DATA.Default);
    }

    private void Update()
    {
        if (!IsMine) return;
        _stateMachine.UpdateState();
    }

    private void FixedUpdate()
    {
        if (!IsMine) return;
        _stateMachine.FixedUpdateState();
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

    public void ChangePlayerData(EPLAYER_DATA eData)
    {
        if(!_playerDataDictionary.ContainsKey(eData))
        {
            string name = $"{eData.ToString()}Data";

            PlayerData playerData = _dataList.Find(n => n.name == name);
            if(playerData != null)
            {
                _playerDataDictionary.Add(eData, playerData);
            }
        }

        CurrentPlayerData = _playerDataDictionary[eData];
    }
}

