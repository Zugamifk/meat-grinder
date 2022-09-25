using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;

public interface ICommand
{
    void Execute(GameModel model);
}
