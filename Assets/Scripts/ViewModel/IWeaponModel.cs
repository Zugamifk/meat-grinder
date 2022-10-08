using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponModel : IIdentifiable, IKeyHolder
{
    Guid CurrentTarget { get; }
}
