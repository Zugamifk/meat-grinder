using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructurePrefabs : ScriptableObject, IPrefabLookup
{
    [SerializeField]
    GameObject[] _prefabs;

    Dictionary<string, GameObject> _prefabNametoPrefab = new();

    public GameObject GetPrefab(string key) => _prefabNametoPrefab[key];

    void OnEnable()
    {
        foreach (var go in _prefabs)
        {
            _prefabNametoPrefab.Add(go.name, go);
        }
        Prefabs.Register<ITileStructure>(this);
    }
}
