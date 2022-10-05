using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] Transform _spawnPosition;

    public Transform SpawnPosition => _spawnPosition;
}
