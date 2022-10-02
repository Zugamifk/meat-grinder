using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public partial class GameModel : IGameModel
{
    public MapModel Map { get; set; } = new();
    public IdentifiableCollection<EnemyModel> SpawnedEnemies = new();
    public IdentifiableCollection<EnemySpawnModel> Spawns = new();
    public IdentifiableCollection<IBuilding> Buildings = new();

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
    IMapModel IGameModel.Map => Map;
    IWaveModel IGameModel.CurrentWave => CurrentWave;
    IIdentifiableLookup<IEnemySpawnModel> IGameModel.Spawns => Spawns;
    IIdentifiableLookup<IEnemyModel> IGameModel.SpawnedEnemies => SpawnedEnemies;
    ICameraModel IGameModel.Camera => Camera;
    #endregion

}
