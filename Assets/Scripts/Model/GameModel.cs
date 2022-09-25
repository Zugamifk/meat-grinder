using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using ViewModel;

namespace Model
{
    public partial class GameModel : IGameModel
    {
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
        #endregion

    }
}