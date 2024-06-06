using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoSingleton<PoolManager>, IInstanceable
{
    [field:SerializeField] public PoolingListSO PoolingListSO {get; private set;}
    private Dictionary<string, Pool<PoolableMono>> _pools = new Dictionary<string, Pool<PoolableMono>>();

    public void CreateInstance()
    {
        if(NetworkManager.Instance == null)
        {
            Debug.LogError($"NetworkManager is null");
            return;
        }
        PoolingListSO.pairs.ForEach(p => CreatePool(p.prefab, p.count));
    }

    public void CreatePool(PoolableMono prefab, int count = 10)
    {
        Pool<PoolableMono> pool = new Pool<PoolableMono>(prefab, transform, count);
        _pools.Add(prefab.gameObject.name, pool);
    }

    public PoolableMono Pop(string prefabName)
    {
        if (!_pools.ContainsKey(prefabName))
        {
            Debug.LogError($"Prefab does not exist on pool: {prefabName}");
            return null;
        }
        PoolableMono item = _pools[prefabName].Pop();
        item.Init();
        return item;
    }

    public void Push(PoolableMono obj)
    {
        _pools[obj.name].Push(obj);
    }


    public void SettingPoolingList(PoolingListSO poolingListSO)
    {
        PoolingListSO = poolingListSO;
    }
}
