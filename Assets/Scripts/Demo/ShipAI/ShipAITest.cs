using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipAITest
{
    public class ShipAITest : MonoBehaviour
    {
        [Header("Friend Colors")]
        [SerializeField] Transform _friendRoot;
        [SerializeField] Transform _enemyRoot;
        [Header("Patrol Test")]
        [SerializeField] Transform _partrolPointA;
        [SerializeField] Transform _partrolPointB;

        void Start()
        {
            Game.Do(new SpawnNPCShipCommand(true, _friendRoot.position));
            Game.Do(new SpawnNPCShipCommand(false, _enemyRoot.position));

            Game.Do(new SetUpPatrolTest(_partrolPointA.position, _partrolPointB.position));
        }
    }
}