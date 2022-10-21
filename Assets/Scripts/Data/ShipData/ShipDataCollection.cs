using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDataCollection : ScriptableObject, IPrefabLookup, IRegisteredData
{
    [SerializeField]
    private List<ShipData> _shipData;

    Dictionary<string, ShipData> _dataNametoData = new();

    public GameObject GetPrefab(string key) => GetShip(key).Prefab;

    public ShipData GetShip(string key) => _dataNametoData[key];

    void OnEnable()
    {
        foreach (var weapon in _shipData)
        {
            _dataNametoData.Add(weapon.name, weapon);
        }

        Prefabs.Register<IShipModel>(this);
    }
}
