using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BootGame : ICommand
{
    public void Execute(GameModel model)
    {
        Scenes.LoadShipInterior();
        Scenes.LoadNavigation();

        model.GameState.CurrentControlMode = EGameControlMode.Navigation;
    }
}
