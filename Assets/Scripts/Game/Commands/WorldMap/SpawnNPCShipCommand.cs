using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNPCShipCommand : ICommand
{
    protected bool _isFriend;
    protected Vector3 _position;
    protected Guid? _setId;

    public SpawnNPCShipCommand(bool isFriend, Vector3 position, Guid? setId)
    {
        _setId = setId;
        _isFriend = isFriend;
        _position = position;
    }

    public void Execute(GameModel model)
    {
        var shipId = _setId.HasValue ? _setId.Value : Guid.NewGuid();
        new SpawnShipCommand(_isFriend, _position, shipId).Execute(model);

        var ai = new AIModel();
        ai.Id = shipId;
        ai.Behaviour.Key = AIBehaviours.IDLE;
        model.AI.AddItem(ai);
        Game.AddUpdater(new AIBehaviourUpdater(ai.Id));
    }
}
