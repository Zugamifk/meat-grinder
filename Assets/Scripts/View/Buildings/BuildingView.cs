using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingView : MonoBehaviour, IModelView<IBuildingModel>
{
    [SerializeField]
    WeaponAttackRadius _attackRadius;

    Identifiable _identifiable;

    public IBuildingModel GetModel() => Game.Model.Buildings.GetItem(Id);

    public Guid Id => _identifiable.Id;

    void Awake()
    {
        _identifiable = GetComponent<Identifiable>();
    }

    public void InitializeFromModel(IBuildingModel model)
    {
        if (_attackRadius != null)
        {
            _attackRadius.Id = model.Id;
        }
    }

    
}
