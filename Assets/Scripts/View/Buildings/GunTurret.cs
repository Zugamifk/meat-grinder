using AnimationGenerator;
using MeshGenerator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTurret : MeshGeneratorUser
{
    [SerializeField] GeneratedSkinnedMeshRenderer _renderer;

    ScriptedAnimation _animator;

    private void Awake()
    {
        _animator = GetComponent<ScriptedAnimation>();
    }

    private void Start()
    {
        AssignMesh<GunTurretMeshGenerator>(_renderer);
        var data = DataService.GetData<MeshGeneratorDataCollection>().GunTurret;
        _animator.PlayAnimation("Barrel", "", data.ShootBarrelRecoilCurve);
    }
}
