using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWaveCommand : ICommand
{
    public void Execute(GameModel model)
    {
        model.CurrentWave = new()
        {
            EnemiesRemaining = 100,
            SpawnsPerMinute = 120,
            WaveCounter = 0
        };
    }
}