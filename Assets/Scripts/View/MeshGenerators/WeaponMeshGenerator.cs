using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator;
using System;

[MeshGenerator("Weapon")]
public class WeaponMeshGenerator : ModelMeshGenerator<IWeaponModel, WeaponMeshGeneratorData>
{
    protected override void BuildMesh(MeshBuilder builder)
    {
    }

    protected override WeaponMeshGeneratorData LoadData() => DataService.GetData<MeshGeneratorDataCollection>().Weapon;
}
