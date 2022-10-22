using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShipModel : IIdentifiable, IKeyHolder
{
    Vector3 Position { get; }
    float Rotation { get; }
}
