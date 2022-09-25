using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class UpdateEnemyMovementCommand : ICommand
    {
        Guid _id;

        public UpdateEnemyMovementCommand(Guid id) => _id = id;

        public void Execute(GameModel model)
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