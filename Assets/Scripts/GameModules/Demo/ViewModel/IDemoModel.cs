using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.ViewModel
{
    public interface IDemoModel : IRegisteredModel
    {
        string Message { get; }
    }
}
