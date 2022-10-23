using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayerShipCommand : ICommand
{
    public void Execute(GameModel model)
    {
        var id = model.PlayerShipId;
        Game.Do(new SpawnShipCommand(true, Vector3.zero, id));
    }
}
