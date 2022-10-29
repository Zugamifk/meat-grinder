using AI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace Demo
{
    public class SetUpMoveToTest : ICommand
    {
        Vector3 _spawnPosition, _destination;
        public SetUpMoveToTest(Vector3 spawnPosition, Vector3 destination)
        {
            _spawnPosition = spawnPosition;
            _destination = destination;
        }

        public void Execute(GameModel model)
        {
            var id = Guid.NewGuid();

            Game.Do(new SpawnNPCShipCommand(true, _spawnPosition, id));
            Game.Do(new MoveTo(id, _destination));
        }
    }
}
