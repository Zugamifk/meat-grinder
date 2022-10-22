using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyUpdater : IUpdater
{
    Guid _id;

    public EnemyUpdater(Guid id)
    {
        _id = id;
    }

    public void Update(GameModel model)
    {
        UpdateMovement(model);
    }

    void UpdateMovement(GameModel model)
    {
        var enemy = model.SpawnedEnemies.GetItem(_id);

        if (enemy.Movement.CurrentNode == null) return;

        var data = DataService.GetData<EnemyPrefabs>().GetData(enemy.Key);
        var stepDist = data.MoveSpeed * model.TimeModel.LastDeltaTime;
        StepAlongPath(enemy, stepDist);
    }

    void StepAlongPath(EnemyModel enemy, float movementRemaining)
    {
        var toEnd = enemy.Movement.CurrentNode.WorldPosition - enemy.Movement.CurrentPosition;
        if (toEnd.magnitude <= movementRemaining)
        {
            enemy.Movement.CurrentPosition = enemy.Movement.CurrentNode.WorldPosition;
            enemy.Movement.CurrentNode = enemy.Movement.CurrentNode.Next;
            if (enemy.Movement.CurrentNode != null)
            {
                movementRemaining -= toEnd.magnitude;
                StepAlongPath(enemy, movementRemaining);
            }
        }
        else
        {
            // step along path
            var dir = toEnd.normalized;
            enemy.Movement.CurrentPosition += dir * movementRemaining;
        }
    }
}
