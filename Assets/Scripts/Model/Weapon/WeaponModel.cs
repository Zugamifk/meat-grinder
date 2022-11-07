using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class WeaponModel : IWeaponModel
    {
        public Guid Id { get; set; }
        public string Key { get; set; }

        // move these to an AI behaviour model
        public Guid CurrentTarget { get; set; }
        public float ShotTimer { get; set; }
    }
}