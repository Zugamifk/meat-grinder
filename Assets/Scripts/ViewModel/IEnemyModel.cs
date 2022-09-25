using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyModel : IIdentifiable, IKeyHolder
{
    Vector3 Position { get; }
}
