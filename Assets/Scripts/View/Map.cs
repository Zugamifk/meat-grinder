using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    class DefaultEdgeTile : ITileModel
    {
        public bool HasPath => false;
        public int Height => 1;

        public ITileStructure Structure => null;

        public static readonly DefaultEdgeTile Instance = new();
    }

    [SerializeField]
    MapTile _tilePrefab;

    Guid _mapGuid;

    Dictionary<Vector2Int, MapTile> _positionToTile = new();

    public MapTile GetTile(Vector2Int position) => _positionToTile[position];

    private void Update()
    {
        var map = Game.Model.Map;
        if (_mapGuid != map.Id)
        {
            UpdateMap(map);
        }
    }

    void UpdateMap(IMapModel map)
    {
        ITileModel left, top, right, bottom;

        for (int x = map.Bounds.xMin; x <= map.Bounds.xMax; x++)
        {
            for (int y = map.Bounds.yMin; y <= map.Bounds.yMax; y++)
            {
                if (x > map.Bounds.xMin)
                {
                    left = map.GetTile(new Vector2Int(x - 1, y));
                }
                else left = DefaultEdgeTile.Instance;
                if (x < map.Bounds.xMax)
                {
                    right = map.GetTile(new Vector2Int(x + 1, y));
                }
                else right = DefaultEdgeTile.Instance;
                if (y > map.Bounds.yMin)
                {
                    bottom = map.GetTile(new Vector2Int(x, y - 1));
                }
                else bottom = DefaultEdgeTile.Instance;
                if (y < map.Bounds.yMax)
                {
                    top = map.GetTile(new Vector2Int(x, y + 1));
                }
                else top = DefaultEdgeTile.Instance;

                var tile = Instantiate(_tilePrefab);
                tile.name = $"Tile ({x}, {y})";
                tile.transform.parent = transform;
                tile.transform.position = new Vector3(x, 0, y);

                var context = new MapMeshGenerator.TileContext()
                {
                    LeftNeighbour = left,
                    TopNeighbour = top,
                    RightNeighbour = right,
                    BottomNeighbour = bottom
                };
                tile.SetTile(map.GetTile(new Vector2Int(x, y)), context);
                _positionToTile[new Vector2Int(x, y)] = tile;
            }
        }

        _mapGuid = map.Id;
    }
}
