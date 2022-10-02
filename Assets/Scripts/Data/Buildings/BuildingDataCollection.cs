using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDataCollection : ScriptableObject, IPrefabLookup
{
    [SerializeField]
    private List<BuildingData> _buildingData;

    Dictionary<string, BuildingData> _prefabNametoPrefab = new();

    public BuildingData GetBuilding(string key) => _prefabNametoPrefab[key];
    public GameObject GetPrefab(string key) => GetBuilding(key).Prefab;

    void OnEnable()
    {
        foreach (var building in _buildingData)
        {
            _prefabNametoPrefab.Add(building.name, building);
        }
        Prefabs.Register<IBuildingModel>(this);
    }
}
