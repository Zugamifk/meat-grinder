using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UpdateWaveCommand : ICommand
{
    public void Execute(GameModel model)
    {
        var dt = model.TimeModel.LastDeltaTime;
        var wave = model.CurrentWave;
        var spawns = model.Spawns.AllItems;
        foreach (var s in spawns)
        {
            s.SpawnQueue.Clear();
        }

        for (wave.WaveCounter += dt * (wave.SpawnsPerMinute / 60); wave.WaveCounter > 1 && wave.EnemiesRemaining > 0; wave.WaveCounter--, wave.EnemiesRemaining--)
        {
            var spawn = spawns.ElementAt(Random.Range(0, spawns.Count()));
            var enemy = new EnemyModel();
            enemy.Movement.CurrentNode = spawn.PathNode.Next;
            enemy.Movement.CurrentPosition = spawn.PathNode.WorldPosition;
            model.SpawnedEnemies.AddItem(enemy);
            spawn.SpawnQueue.Add(enemy.Id);
        }
    }
}
