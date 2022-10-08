using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePrefabs : ScriptableObject, IPrefabLookup, IRegisteredData
{
    [SerializeField]
    GameObject[] _prefabs;
    Dictionary<string, GameObject> _keyToPrefab = new();

    public GameObject GetPrefab(string key) => _keyToPrefab[key];

    private void OnEnable()
    {
        foreach (var p in _prefabs)
        {
            _keyToPrefab.Add(p.name, p);
        }

        Prefabs.Register<IProjectileModel>(this);
    }
}
