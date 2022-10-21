using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipModel : IShipModel
{
    public Guid Id { get; } = Guid.NewGuid(); 

    public string Key { get; set; }
}
