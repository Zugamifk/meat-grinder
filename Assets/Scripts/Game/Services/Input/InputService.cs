using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputService
{
    Dictionary<string, IInputHandler> _idToHandler = new();

    public InputService()
    {
        _idToHandler["WorldMap"] = new WorldMapInputBehaviour();
    }

    public void HandleClick(string handlerKey, Guid targetId)
    {
        if(!_idToHandler.ContainsKey(handlerKey))
        {
            throw new ArgumentException($"No input handler for key {handlerKey}");
        }

        var handler = _idToHandler[handlerKey];
        handler.HandleClick(targetId);
    }
}
