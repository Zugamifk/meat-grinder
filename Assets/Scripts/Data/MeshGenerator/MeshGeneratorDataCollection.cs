using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGeneratorDataCollection : ScriptableObject, IRegisteredData
{
    public ArrowTowerMeshGeneratorData ArrowTower;
    public EndPortalMeshGeneratorData EndPortal;
    public EnemyMeshGeneratorData Enemy;
    public GearMeshGeneratorData Gear;
    public GunTurretMeshGeneratorData GunTurret;
    public TileMeshGeneratorData Tile;
    public WorldMapMeshGeneratorData WorldMap;
    public GearAssemblyMeshGeneratorData GearAssembly;
}