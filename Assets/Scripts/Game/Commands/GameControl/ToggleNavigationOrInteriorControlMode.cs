using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public struct ToggleNavigationOrInteriorControlMode : ICommand
{
    public void Execute(GameModel model)
    {
        var currentMode = model.GameState.CurrentControlMode;
        var nextMode = EGameControlMode.None;
        if(currentMode == EGameControlMode.ShipInterior)
        {
            nextMode = EGameControlMode.Navigation;
        } else if (currentMode == EGameControlMode.Navigation)
        {
            nextMode = EGameControlMode.ShipInterior;
        } else
        {
            throw new System.InvalidOperationException($"Can't toggle state from {currentMode}");
        }

        new SetGameControlMode(nextMode).Execute(model);
    }
}
