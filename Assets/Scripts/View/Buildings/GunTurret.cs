using AnimationGenerator;
using MeshGenerator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class GunTurret : MeshGeneratorUser
{
    [SerializeField] GeneratedSkinnedMeshRenderer _renderer;
    [SerializeField] Transform _gunRoot;

    ScriptedAnimation _animator;

    Identifiable _identifiable;

    private void Awake()
    {
        _animator = GetComponent<ScriptedAnimation>();
        _identifiable = GetComponent<Identifiable>();
    }

    private void Start()
    {
        AssignMesh<GunTurretMeshGenerator>(_renderer);
        var data = DataService.GetData<MeshGeneratorDataCollection>().GunTurret;
        _animator.PlayAnimation("Barrel", "", data.ShootBarrelRecoilCurve);
    }

    private void Update()
    {
        if (_identifiable.Id == Guid.Empty)
        {
            return;
        }

        var model = Game.Model.Buildings.GetItem(_identifiable.Id);
        if (model == null)
        {
            throw new InvalidOperationException($"No building with id {_identifiable.Id}");
        }

        Game.Do(new UpdateBuildingTargetCommand(_identifiable.Id));
        if(model?.CurrentTarget!=Guid.Empty)
        {
            UpdateTarget(model.CurrentTarget);
        }
    }

    void UpdateTarget(Guid targetId)
    {
        var target = ViewLookup.Get(targetId);
        if (target != null)
        {
            Debug.Log("target: " + targetId);

            transform.LookAt(target.transform);
        }
    }
}
