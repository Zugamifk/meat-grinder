using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGeneratorDataCollection : ScriptableObject, IRegisteredData
{
    public ArrowTowerMeshGeneratorData ArrowTower;
    public EndPortalMeshGeneratorData EndPortal;
    public EnemyMeshGeneratorData Enemy;
    public GearAssemblyMeshGeneratorData GearAssembly;
    public GearMeshGeneratorData Gear;
    public GunTurretMeshGeneratorData GunTurret;
    public ShipMeshGeneratorData Ship;
    public TileMeshGeneratorData Tile;
    public WorldMapMeshGeneratorData WorldMap;
    public WeaponMeshGeneratorData Weapon;
}