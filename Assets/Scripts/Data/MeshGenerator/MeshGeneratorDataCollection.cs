using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGeneratorDataCollection : ScriptableObject, IRegisteredData
{
    public EndPortalMeshGeneratorData EndPortal;
    public EnemyMeshGeneratorData Enemy;
    public ArrowTowerMeshGeneratorData ArrowTower;
    public CardFrameMeshGeneratorData CardFrame;
}
