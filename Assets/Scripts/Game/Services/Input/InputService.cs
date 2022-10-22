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

    public void HandleClick(ClickInfo clickInfo)
    {
        if(!_idToHandler.ContainsKey(clickInfo.InputHandlerKey))
        {
            throw new ArgumentException($"No input handler for key {clickInfo.InputHandlerKey}");
        }

        var handler = _idToHandler[clickInfo.InputHandlerKey];
        handler.HandleClick(clickInfo);
    }
}
