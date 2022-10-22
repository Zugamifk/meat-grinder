using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputModel : IInputModel
{
    public Guid WorldMapInputHandlerId { get; } = Guid.NewGuid(); 
}
