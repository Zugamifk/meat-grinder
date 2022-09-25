using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPrefabs : ScriptableObject, IPrefabLookup, IRegisteredData
{
    [SerializeField]
    GameObject[] _prefabs;
    [SerializeField]
    EnemyData[] _enemyData;

    Dictionary<string, GameObject> _keyToPrefab = new();
    Dictionary<string, EnemyData> _keyToData = new();

    private void OnEnable()
    {
        foreach (var p in _prefabs)
        {
            _keyToPrefab.Add(p.name, p);
        }

        foreach (var d in _enemyData)
        {
            _keyToData.Add(d.Key, d);
        }
        Prefabs.Register<IEnemyModel>(this);
    }

    public GameObject GetPrefab(string key) => _keyToPrefab[key];
    public EnemyData GetData(string key) => _keyToData[key];
}
