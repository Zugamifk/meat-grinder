using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReachEndCommand : ICommand
{
    Guid _id;
    public EnemyReachEndCommand(Guid id) => _id = id;

    public void Execute(GameModel model)
    {
        model.SpawnedEnemies.RemoveItem(_id);
    }
}
