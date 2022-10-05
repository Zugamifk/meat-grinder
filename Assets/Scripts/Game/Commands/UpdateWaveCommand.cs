using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UpdateWaveCommand : ICommand
{
    public void Execute(GameModel model)
    {
        if (model.Spawns.IsEmpty) return;

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
            Game.Do(new SpawnEnemyCommand(spawn.Id));    
        }
    }
}
