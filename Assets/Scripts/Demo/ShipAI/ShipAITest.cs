using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipAITest
{
    public class ShipAITest : MonoBehaviour
    {
        [Header("Patrol Test")]
        [SerializeField] Transform _partrolPointA;
        [SerializeField] Transform _partrolPointB;

        void Start()
        {
            //Game.Do(new SpawnNPCShipCommand(true, new Vector3(-5, 0, 0)));
            //Game.Do(new SpawnNPCShipCommand(false, new Vector3(5, 0, 0)));

            Game.Do(new SetUpPatrolTest(_partrolPointA.position, _partrolPointB.position));
        }
    }
}