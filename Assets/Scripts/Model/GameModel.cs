using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public partial class GameModel : IGameModel
{
    public ShipMapModel ShipMap { get; set; } = new();
    public IdentifiableCollection<EnemyModel> SpawnedEnemies = new();
    public IdentifiableCollection<EnemySpawnModel> Spawns = new();
    public IdentifiableCollection<BuildingModel> Buildings = new();
    public IdentifiableCollection<WeaponModel> Weapons = new();
    public IdentifiableCollection<ProjectileModel> Projectiles = new();
    public IdentifiableCollection<ShipModel> Ships = new();

    public WaveModel CurrentWave { get; set; }
    public CameraModel Camera = new() { Height = 15, MoveSpeed = 5, RotateSpeed = 45 };

    public TimeModel TimeModel = new TimeModel();
    public Dictionary<Type, object> TypeToModel = new();

    public TModel GetModel<TModel>()
        where TModel : IRegisteredModel
    {
        if (TypeToModel.TryGetValue(typeof(TModel), out object model))
        {
            return (TModel)model;
        }
        else
        {
            return default;
        }
    }

    public TModel CreateModel<TModel>()
        where TModel : IRegisteredModel, new()
    {
        var result = new TModel();
        SetModel(result);
        return result;
    }

    public void SetModel<TModel>(TModel model)
        where TModel : IRegisteredModel
    {
        TypeToModel[typeof(TModel)] = model;
        foreach (var i in typeof(TModel).GetInterfaces())
        {
            if (typeof(IRegisteredModel).IsAssignableFrom(i))
            {
                TypeToModel[i] = model;
            }
        }
    }

    #region IGameModel
    ITimeModel IGameModel.Time => TimeModel;
    IShipMapModel IGameModel.ShipMap => ShipMap;
    IWaveModel IGameModel.CurrentWave => CurrentWave;
    IIdentifiableLookup<IEnemySpawnModel> IGameModel.Spawns => Spawns;
    IIdentifiableLookup<IEnemyModel> IGameModel.Enemies => SpawnedEnemies;
    IIdentifiableLookup<IBuildingModel> IGameModel.Buildings => Buildings;
    IIdentifiableLookup<IWeaponModel> IGameModel.Weapons => Weapons;
    IIdentifiableLookup<IProjectileModel> IGameModel.Projectiles => Projectiles;
    IIdentifiableLookup<IShipModel> IGameModel.Ships => Ships;

    ICameraModel IGameModel.Camera => Camera;
    #endregion

}
