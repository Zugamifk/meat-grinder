using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MeshGeneratorUser, IModelView<IShipModel>
{
    [SerializeField]
    MeshFilter _meshFilter;
    [SerializeField]
    InputHandler _inputHandler;

    Identifiable _identifiable;

    public Guid Id => _identifiable.Id;

    public void InitializeFromModel(IShipModel model)
    {
        _identifiable.Id = model.Id;
        _inputHandler.Id = model.Id;
    }

    void Awake()
    {
        _identifiable = GetComponent<Identifiable>();
    }

    private void Start()
    {
        AssignMesh<ShipMeshGenerator>(_meshFilter);
    }
}
