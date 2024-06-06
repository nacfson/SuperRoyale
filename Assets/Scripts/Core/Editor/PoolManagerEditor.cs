using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(PoolManager))]
public class PoolManagerEditor : Editor
{
    public PoolManager manager;
    private static string s_folderPath = "Assets/ScriptableObject/PoolingList/";


    private void OnEnable()
    {
        manager = target as PoolManager;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Reload"))
        {
            Debug.Log($"PoolingCnt: {LoadPoolingList()}");
        }
    }

    private int LoadPoolingList()
    {
        PoolingListSO poolingList = ScriptableObject.CreateInstance<PoolingListSO>();


        string assetPath = s_folderPath + "PoolingList.asset";
        if (!AssetDatabase.IsValidFolder(s_folderPath))
        {
            PoolingListSO originList = AssetDatabase.LoadAssetAtPath<PoolingListSO>(assetPath);
            if (originList != null)
            {
                poolingList = originList;
            }
        }


        AssetDatabase.CreateAsset(poolingList, assetPath);

        string[] guids = AssetDatabase.FindAssets("t:Prefab");

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (prefab != null)
            {
                if (prefab.TryGetComponent(out PoolableMono poolableMono))
                {
                    if (poolableMono.poolingCount <= 0) continue;
                    PoolingPair poolingItem = new PoolingPair{ prefab = poolableMono, count = poolableMono.poolingCount };
                    poolingList.pairs.Add(poolingItem);
                }
            }
        }
        manager.SettingPoolingList(poolingList);
        EditorUtility.SetDirty(poolingList);
        return poolingList.pairs.Count;
    }
}
#endif