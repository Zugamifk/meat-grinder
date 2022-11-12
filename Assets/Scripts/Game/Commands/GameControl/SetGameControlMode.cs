using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SetGameControlMode : ICommand
{
    EGameControlMode _controlMode;
    public SetGameControlMode(EGameControlMode controlMode)
    {
        _controlMode = controlMode;
    }

    public void Execute(GameModel model)
    {
        Debug.Log($"{model.GameState.CurrentControlMode} -> {_controlMode}");
        model.GameState.CurrentControlMode = _controlMode;
    }
}
