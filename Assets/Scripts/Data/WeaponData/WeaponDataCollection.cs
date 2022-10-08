using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDataCollection : ScriptableObject, IRegisteredData
{
    [SerializeField]
    private List<WeaponData> _buildingData;

    Dictionary<string, WeaponData> _dataNametoData = new();

    public WeaponData GetWeapon(string key) => _dataNametoData[key];

    void OnEnable()
    {
        foreach (var weapon in _buildingData)
        {
            _dataNametoData.Add(weapon.name, weapon);
        }
    }
}
