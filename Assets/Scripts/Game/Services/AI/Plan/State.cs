using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public sealed class State
    {
        public Dictionary<string, bool> Values { get; } = new();

        /// <summary>
        /// return true if this state contains all keys in condition and all the values match
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool InState(State condition)
        {
            foreach(var k in condition.Values.Keys)
            {
                if(!Values.TryGetValue(k, out bool value) || !value)
                {
                    return false;
                }
            }
            return true;
        }

        public void AddValue(string key, bool value)
        {
            Values.Add(key, value);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Values);
        }
    }
}