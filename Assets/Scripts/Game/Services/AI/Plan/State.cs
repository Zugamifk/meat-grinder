using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    internal sealed class State
    {
        public Dictionary<string, bool> States { get; } = new();

        /// <summary>
        /// return true if this state contains all keys in condition and all the values match
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool InState(State condition)
        {
            foreach(var k in condition.States.Keys)
            {
                if(!States.TryGetValue(k, out bool value) || !value)
                {
                    return false;
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(States);
        }
    }
}