using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile : MeshGeneratorUser
{
    [SerializeField]
    MeshFilter _meshFilter;
    [SerializeField]
    BoxCollider _collider;
    [SerializeField]
    Transform _containedObjectsRoot;

    BuildingView _currentBuildingView;

    public float SurfaceY { get; private set; }

    public void SetTile(ITileModel tile)
    {
        UpdateTileGeometry(tile);
    }

    public void AddBuilding(BuildingView building)
    {
        _currentBuildingView = building;
        building.transform.parent = _containedObjectsRoot;
        building.transform.localPosition = Vector3.zero;
    }

    void UpdateTileGeometry(ITileModel tile)
    {
        var data = DataService.GetData<GameData>();
        SurfaceY = tile.Height * data.TileStepHeight;

        AssignMesh<TileMeshGenerator>(_meshFilter, gen => gen.SetTile(tile));

        _collider.center = new Vector3(0, SurfaceY / 2f, 0);
        _collider.size = new Vector3(1, SurfaceY, 1);
        _containedObjectsRoot.localPosition = new Vector3(0, SurfaceY, 0);
    }
}
