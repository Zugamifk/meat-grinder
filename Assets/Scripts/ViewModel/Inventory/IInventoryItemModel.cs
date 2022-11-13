using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryItemModel : IKeyHolder, IIdentifiable
{
    string DisplayName { get; }
}
