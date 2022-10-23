using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionDemo : MonoBehaviour
{
    void Start()
    {
        Game.Do(new SpawnNPCShipCommand(true, Vector3.zero));
    }
}
