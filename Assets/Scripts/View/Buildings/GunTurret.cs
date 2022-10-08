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
        _animator.SetAnimation("Barrel", data.ShootBarrelRecoilCurve);
    }

    private void Update()
    {
        if (_identifiable.Id == Guid.Empty)
        {
            return;
        }

        var weapon = Game.Model.Weapons.GetItem(_identifiable.Id);
        if (weapon == null)
        {
            throw new InvalidOperationException($"No building with id {_identifiable.Id}");
        }

        Game.Do(new UpdateWeaponShotCooldownCommand(_identifiable.Id));
        Game.Do(new UpdateBuildingTargetCommand(_identifiable.Id));
        if(weapon?.CurrentTarget!=Guid.Empty)
        {
            UpdateTarget(weapon.CurrentTarget);

            if (weapon.ShotTimer <= 0)
            {
                Fire();
            }
        }
    }

    void UpdateTarget(Guid targetId)
    {
        var target = ViewLookup.Get(targetId);
        if (target != null)
        {
            transform.LookAt(target.transform);
        }
    }

    void Fire()
    {
        Game.Do(new ShootWeaponCommand(_identifiable.Id));
        _animator.PlayAnimation();
    }
}
