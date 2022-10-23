using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControlTest : MonoBehaviour
{
    void Start()
    {
        Game.Do(new SpawnPlayerShipCommand());
    }
}
