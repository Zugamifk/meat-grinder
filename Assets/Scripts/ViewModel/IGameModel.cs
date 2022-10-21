using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameModel
{
    ITimeModel Time { get; }
    TModel GetModel<TModel>() where TModel : IRegisteredModel;
    IShipMapModel ShipMap { get; }
    IWaveModel CurrentWave { get; }
    IIdentifiableLookup<IEnemyModel> Enemies { get; }
    IIdentifiableLookup<IEnemySpawnModel> Spawns { get; }
    IIdentifiableLookup<IBuildingModel> Buildings { get; }
    IIdentifiableLookup<IWeaponModel> Weapons { get; }
    IIdentifiableLookup<IProjectileModel> Projectiles { get; }
    IIdentifiableLookup<IShipModel> Ships { get; }
    ICameraModel Camera { get; }
}
