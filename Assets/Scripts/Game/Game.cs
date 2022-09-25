using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Model;
using ViewModel;

public class Game : MonoBehaviour
{
    [SerializeField]
    DataReferences _gameData;

    GameModel _model = new GameModel();

    static Game _game;
    static Queue<ICommand> _commandQueue = new Queue<ICommand>();
    public static IGameModel Model => _game._model;

    public static void Do(ICommand command)
    {
        _commandQueue.Enqueue(command);
    }

    Game()
    {
        _game = this;

        InitializeTimeModel();
    }

    private void Update()
    {
        UpdateTimeModel();

        while (_commandQueue.Count > 0)
        {
            var command = _commandQueue.Dequeue();
            command.Execute(_model);
        }
    }

    void InitializeTimeModel()
    {
        var timeModel = _model.TimeModel;
        var time = DateTime.Now;
        timeModel.RealTime = TimeSpan.FromSeconds(time.TimeOfDay.TotalSeconds / TimeModel.TIME_MULTIPLIER);
    }

    void UpdateTimeModel()
    {
        var timeModel = _model.TimeModel;
        timeModel.LastDeltaTime = Time.deltaTime;
        timeModel.RealTime += TimeSpan.FromSeconds(Time.deltaTime);
    }
}
