using StateMachines;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NavigationState : TopDownCameraState
{
    public override IState UpdateState()
    {
        HandleKeyboardInput();
        HandleMouseInput();

        return this;
    }
}
