using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAITest : MonoBehaviour
{
    void Start()
    {
        Game.Do(new SpawnNPCShipCommand(true, new Vector3(-5,0,0)));
        Game.Do(new SpawnNPCShipCommand(false, new Vector3(5,0,0)));
    }
}
