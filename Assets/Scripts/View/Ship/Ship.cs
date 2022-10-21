using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MeshGeneratorUser, IModelView<IShipModel>
{
    [SerializeField]
    MeshFilter _meshFilter;

    Identifiable _identifiable;

    public Guid Id => _identifiable.Id;

    public void InitializeFromModel(IShipModel model)
    {
        _identifiable.Id = model.Id;
    }

    private void Start()
    {
        AssignMesh<ShipMeshGenerator>(_meshFilter);
    }
}
