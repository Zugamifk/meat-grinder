using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMapTest : MonoBehaviour
{
    [SerializeField]
    Transform _spawnedRoot;
    [SerializeField]
    ShipMap _map;

    public ShipMap Map => _map;
    public Transform SpawnRoot => _spawnedRoot;

    public static ShipMapTest Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Game.Do(new GenerateMapCommand());
        Game.Do(new SpawnBuildingCommand("GunTurret", new Vector2Int(0,0)));
        Game.Do(new StartWaveCommand());
    }
}
