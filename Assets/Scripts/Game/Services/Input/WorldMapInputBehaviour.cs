using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapInputBehaviour : IInputHandler
{
    public void HandleClick(ClickInfo clickInfo)
    {
        Game.Do(new SetShipDestinationCommand(Game.Model.PlayerShipId, clickInfo.ClickPosition));
    }
}
