using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMapCommand : ICommand
{
    public void Execute(GameModel model)
    {
        var generator = new MapGenerator();
        generator.GenerateMap(model);
    }
}
