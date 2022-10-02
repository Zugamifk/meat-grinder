using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] Transform _spawnPosition;

    Identifiable _identifiable;

    private void Awake()
    {
        _identifiable = GetComponent<Identifiable>();
    }

    private void Update()
    {
        foreach (var e in Game.Model.Spawns.GetItem(_identifiable.Id).SpawnQueue)
        {
            var enemy = Game.Model.SpawnedEnemies.GetItem(e);
            SpawnEnemy(enemy);
        }
    }

    void SpawnEnemy(IEnemyModel enemy)
    {
        var enemyPrefab = Prefabs.GetInstance(enemy);
        enemyPrefab.transform.SetParent(Test.Instance.SpawnRoot);
        enemyPrefab.transform.position = _spawnPosition.position;

        var identifiable = enemyPrefab.GetComponent<Identifiable>();
        identifiable.Id = enemy.Id;
    }
}
