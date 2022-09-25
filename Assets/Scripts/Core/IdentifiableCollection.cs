using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IdentifiableCollection<TModel> : IIdentifiableLookup<TModel>
    where TModel : IIdentifiable
{
    Dictionary<Guid, TModel> _identifiables = new Dictionary<Guid, TModel>();
    Dictionary<string, Guid> _keyholderKeyToId = new Dictionary<string, Guid>();

    public IEnumerable<TModel> AllItems => _identifiables.Values;

    public TModel this[string key] => GetItem(key);
    public TModel this[Guid id] => GetItem(id);

    public void AddItem(TModel model)
    {
        if(model is IKeyHolder keyHolder )
        {
            _keyholderKeyToId[keyHolder.Key] = model.Id;
        }

        _identifiables[model.Id] = model;
    }

    public void RemoveItem(Guid id)
    {
        _identifiables.Remove(id);
    }

    public void RemoveItem(string key)
    {
        var id = _keyholderKeyToId[key];
        RemoveItem(id);
    }

    public TModel GetItem(Guid id)
    {
        if(_identifiables.TryGetValue(id, out TModel value))
        {
            return value;
        } else
        {
            return default;
        }
    }

    public TModel GetItem(string key)
    {
        var id = _keyholderKeyToId[key];
        return GetItem(id);
    }

    public bool HasId(Guid id)
    {
        return _identifiables.ContainsKey(id);
    }

    public bool HasUniqueName(string key)
    {
        return _keyholderKeyToId.ContainsKey(key);
    }
}
