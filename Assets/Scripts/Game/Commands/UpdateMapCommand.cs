using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateMapCommand : ICommand
{
    public void Execute(GameModel model)
    {
        model.Map.DirtyTiles.Clear();
    }
}
