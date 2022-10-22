using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickedWorldObjectCommand : ICommand
{
    static InputService _inputService = new();
    string _handlerKey;
    Guid _targetId;
    public ClickedWorldObjectCommand(string handlerKey, Guid targetId)
    {
        if (string.IsNullOrEmpty(handlerKey))
        {
            throw new InvalidOperationException("Handler key is empty!");
        }

        _handlerKey = handlerKey;
        _targetId = targetId;
    }

    public void Execute(GameModel model)
    {
        _inputService.HandleClick(_handlerKey, _targetId);
    }
}
