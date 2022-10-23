using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShipAITest
{
    public class SetUpPatrolTest : ICommand
    {
        Vector3 _start, _end;

        public SetUpPatrolTest(Vector3 start, Vector3 end)
        {
            _start = start;
            _end = end;
        }

        public void Execute(GameModel model)
        {
            var id = Guid.NewGuid();

            Game.Do(new SpawnNPCShipCommand(true, _start, id));
            Game.Do(new AIStartPatrolCommand(id, _start, _end));
        }
    }
}