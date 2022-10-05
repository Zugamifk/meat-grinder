using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingView : MonoBehaviour, IModelView<IBuildingModel>
{
    [SerializeField]
    WeaponAttackRadius _attackRadius;

    Identifiable _identifiable;

    public Guid Id => _identifiable.Id;

    void Awake()
    {
        _identifiable = GetComponent<Identifiable>();
    }

    public void InitializeFromModel(IBuildingModel model)
    {
        Debug.Log("Spawned " + model.Key);

        if (_attackRadius != null)
        {
            _attackRadius.Id = model.Id;
        }
    }
}
