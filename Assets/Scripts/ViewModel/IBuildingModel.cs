using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuildingModel : IKeyHolder, IIdentifiable
{
    Vector2Int TilePosition { get; }
}
