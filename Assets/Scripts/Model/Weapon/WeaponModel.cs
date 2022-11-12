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

        public bool Loaded { get; set; }
        public AssemblyModel Assembly { get; }

        // move these to an AI behaviour model
        public Guid CurrentTarget { get; set; }
        public float ShotTimer { get; set; }
    }
}