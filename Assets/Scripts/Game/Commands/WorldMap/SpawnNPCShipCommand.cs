using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNPCShipCommand : ICommand
{
    public void Execute(GameModel model)
    {
        Game.Do(new SpawnShipCommand(Guid.NewGuid()));
    }
}
