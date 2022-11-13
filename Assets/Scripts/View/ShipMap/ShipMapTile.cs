using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMapTile : MeshGeneratorUser
{
    [SerializeField]
    MeshFilter _meshFilter;
    [SerializeField]
    BoxCollider _collider;
    [SerializeField]
    Transform _containedObjectsRoot;

    public void SetTile(ITileModel tile)
    {
        UpdateTileGeometry(tile);
    }

    public void AddBuilding(BuildingView building)
    {
        building.transform.parent = _containedObjectsRoot;
        building.transform.localPosition = Vector3.zero;
    }

    void UpdateTileGeometry(ITileModel tile)
    {
        var y = tile.Height;
        if(tile.Type == ETileType.Wall)
        {
            y += 1;
        }

        AssignMesh<TileMeshGenerator>(_meshFilter, gen => gen.SetTile(tile));

        _meshFilter.transform.localPosition = new Vector3(0, -y, 0);

        _collider.center = new Vector3(0, -y/2, 0);
        _collider.size = new Vector3(1, y, 1);
    }
}
