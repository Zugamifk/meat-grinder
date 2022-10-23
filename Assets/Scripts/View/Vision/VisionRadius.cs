using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionRadius : MonoBehaviour
{
    [SerializeField]
    SphereCollider _collider;

    public Guid Id;

    bool _targetsChanged = false;
    HashSet<Guid> _targets = new HashSet<Guid>();

    private void Update()
    {
        var vision = Game.Model.Vision.GetItem(Id);
        _collider.radius = vision.Range;

        if(_targetsChanged)
        {
            Game.Do(new SetObjectsInVisionRangeCommand(Id, _targets));
            _targetsChanged = false;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        var enemy = collision.GetComponent<Enemy>();
        if (enemy!=null)
        {
            _targets.Add(enemy.Id);
            _targetsChanged = true;
        }
    }

    private void OnTriggerExit(Collider collision)
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
