using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraCommand : ICommand
{
    Vector2 _delta;
    public MoveCameraCommand(Vector2 delta) => _delta = delta;
    public void Execute(GameModel model)
    {
        var moveDelta = Quaternion.Euler(0, 0, -model.Camera.Angle) * _delta;
        model.Camera.Position += (Vector2)moveDelta * model.Camera.MoveSpeed * model.TimeModel.LastDeltaTime;
    }
}
