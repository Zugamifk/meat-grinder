using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapInputBehaviour : IInputHandler
{
    public void HandleClick(Vector3 position)
    {
        Game.Do(new SetShipDestinationCommand(Game.Model.PlayerShipId, position));
    }
}
