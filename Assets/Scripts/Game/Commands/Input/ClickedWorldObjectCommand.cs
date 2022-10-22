using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickedWorldObjectCommand : ICommand
{
    static InputService _inputService = new();
    
    ClickInfo _clickInfo;

    public ClickedWorldObjectCommand(ClickInfo clickInfo)
    {
        if (string.IsNullOrEmpty(clickInfo.InputHandlerKey))
        {
            throw new InvalidOperationException("Handler key is empty!");
        }

        _clickInfo = clickInfo;
    }

    public void Execute(GameModel model)
    {
        _inputService.HandleClick(_clickInfo);
    }
}
