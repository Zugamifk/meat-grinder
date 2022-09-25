using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCameraCommand : ICommand
{
    float _delta;
    public RotateCameraCommand(float delta) => _delta = delta;

    public void Execute(GameModel model)
    {
        model.Camera.Angle += _delta * model.Camera.RotateSpeed * model.TimeModel.LastDeltaTime;
    }
}
