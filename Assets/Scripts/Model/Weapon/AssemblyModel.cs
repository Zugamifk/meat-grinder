using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class AssemblyModel
    {
        public EWeaponCalibre Calibre => Receiver.Calibre;
        public EMountType Mount { get; set; }
        public ReceiverModel Receiver { get;set; }
        public BarrelModel Barrel { get;set; }
        public AmmunitionModel Ammunition { get; set; }
        public int AmmunitionCount { get; set; }
    }
}