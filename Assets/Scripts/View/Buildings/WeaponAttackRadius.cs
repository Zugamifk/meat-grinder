using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttackRadius : MonoBehaviour
{
    Identifiable _identifiable_cached;
    Guid _id => (_identifiable_cached ??= GetComponent<Identifiable>()).Id;

    bool _targetsChanged = false;
    HashSet<Guid> _targets = new HashSet<Guid>();

    private void Update()
    {
        if(_targetsChanged)
        {
            Game.Do(new UpdateEnemiesInRangeCommand(_id, _targets));
            _targetsChanged = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.GetComponent<Enemy>();
        if(enemy!=null)
        {
            _targets.Add(enemy.Id);
            _targetsChanged = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            _targets.Remove(enemy.Id);
            _targetsChanged = true;
        }
    }

    private void OnDrawGizmos()
    {
        
    }
}
