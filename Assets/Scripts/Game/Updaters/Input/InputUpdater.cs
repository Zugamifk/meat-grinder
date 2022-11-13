using StateMachines;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputUpdater : IUpdater
{
    StateMachine _inputStateMachine;
    InputStateContext _inputStateContext;

    public InputUpdater()
    {
        _inputStateContext = new();
        _inputStateContext.IdToHandler[Game.Model.Input.WorldMapInputHandlerId] = new WorldMapInputBehaviour();
        _inputStateMachine = new StateMachine(new NavigationState() { Context = _inputStateContext });
    }

    public void Update(GameModel model)
    {
        _inputStateMachine.Update();
    }
}
