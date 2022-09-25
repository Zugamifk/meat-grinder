using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ViewModel
{
    public interface IGameModel
    {
        ITimeModel Time { get; }
        TModel GetModel<TModel>() where TModel : IRegisteredModel;
    }
}