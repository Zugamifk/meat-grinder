using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapInputBehaviour : IInputHandler
{
    public void HandleClick(ClickInfo clickInfo)
    {
        Debug.Log("Move to " + clickInfo.ClickPosition);
    }
}
