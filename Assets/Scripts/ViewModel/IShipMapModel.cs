using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShipMapModel : IIdentifiable
{
    BoundsInt Bounds { get; }
    ITileModel GetTile(Vector2Int position);
}
