using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAITest : MonoBehaviour
{
    void Start()
    {
        Game.Do(new SpawnNPCShipCommand());
    }
}
