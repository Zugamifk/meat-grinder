using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickedWorldObjectCommand : ICommand
{
    Guid _handlerId;
    public ClickedWorldObjectCommand(Guid handlerId)
    {
        _handlerId = handlerId;
    }

    public void Execute(GameModel model)
    {
        Debug.Log("Clicked " + _handlerId);
    }
}
