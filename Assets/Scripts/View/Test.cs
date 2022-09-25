using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    Transform _spawnedRoot;
    [SerializeField]
    Map _map;

    public Map Map => _map;
    public Transform SpawnRoot => _spawnedRoot;

    public static Test Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Game.Do(new GenerateMapCommand());
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
