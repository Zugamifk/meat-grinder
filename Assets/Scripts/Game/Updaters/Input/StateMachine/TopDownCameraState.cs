using StateMachines;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class TopDownCameraState : InputState
{
    Vector2 _lastMousePosition;

    public override void EnterState()
    {
        _lastMousePosition = Input.mousePosition;
    }

    protected void HandleKeyboardInput()
    {
        var move = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            move.y = 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            move.x = -1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            move.y = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            move.x = 1;
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Game.Do(new ToggleNavigationOrInteriorControlMode());
        }

        if (move != Vector2.zero)
        {
            Game.Do(new MoveCameraCommand(move));
        }
    }

    protected void HandleMouseInput()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            // UI handles itself
            return;
        }

        if (HandleWorldInput())
        {
            return;
        }

        HandleRotateCamera();
    }

    bool HandleWorldInput()
    {
        if (!Input.GetMouseButtonUp(0))
        {
            return false;
        }

        var input = Game.Model.Input;
        if (!Context.IdToHandler.ContainsKey(input.CurrentMouseOverObject))
        {
            throw new ArgumentException($"No input handler for id {input.CurrentMouseOverObject}");
        }

        var handler = Context.IdToHandler[input.CurrentMouseOverObject];
        handler.HandleClick(input.ClickPosition);

        return true;
    }

    void HandleRotateCamera()
    {
        var position = (Vector2)Input.mousePosition;
        if (Input.GetMouseButton(1))
        {
            var delta = position - _lastMousePosition;
            Game.Do(new RotateCameraCommand(delta.x));
        }

        _lastMousePosition = position;
    }
}
