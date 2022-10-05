using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    Transform _spawnedRoot;
    [SerializeField]
    ShipMap _map;

    public ShipMap Map => _map;
    public Transform SpawnRoot => _spawnedRoot;

    public static Test Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Game.Do(new GenerateMapCommand());
        Game.Do(new SpawnBuildingCommand("GunTurret", new Vector2Int(3, 5)));
        Game.Do(new StartWaveCommand());
    }

    private void Update()
    {
        if (Game.Model.CurrentWave != null)
        {
            Game.Do(new UpdateWaveCommand());
        }
    }
}
