using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile : MonoBehaviour
{
    [SerializeField]
    MeshFilter _meshFilter;
    [SerializeField]
    BoxCollider _collider;
    [SerializeField]
    Transform _structureRoot;

    GameObject _currentStructure;

    public float SurfaceY { get; private set; }

    public void SetTile(ITileModel tile, MapMeshGenerator.TileContext context)
    {
        UpdateTileGeometry(tile, context);
        if (tile.Structure != null)
        {
            UpdateTileStructure(tile);
        }
    }

    void UpdateTileGeometry(ITileModel tile, MapMeshGenerator.TileContext context)
    {
        var data = DataService.GetData<GameData>();
        SurfaceY = tile.Height * data.TileStepHeight;

        var meshGen = new MapMeshGenerator();
        var mesh = meshGen.GenerateTileMesh(tile, context);
        _meshFilter.mesh = mesh;

        _collider.center = new Vector3(0, SurfaceY / 2f, 0);
        _collider.size = new Vector3(1, SurfaceY, 1);
        _structureRoot.localPosition = new Vector3(0, SurfaceY, 0);
    }

    void UpdateTileStructure(ITileModel tile)
    {
        if (_currentStructure != null)
        {
            Destroy(_currentStructure);
        }

        var structure = Prefabs.GetInstance(tile.Structure);
        structure.transform.SetParent(_structureRoot);
        structure.transform.localPosition = Vector3.zero;

        var id = structure.GetComponent<Identifiable>();
        id.Id = tile.Structure.Id;

        _currentStructure = structure;
    }
}
