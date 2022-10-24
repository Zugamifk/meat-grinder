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
        if (vision == null) return;
        _collider.radius = vision.Range;

        if(_targetsChanged)
        {
            Game.Do(new SetObjectsInVisionRangeCommand(Id, _targets));
            _targetsChanged = false;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        var target = collision.GetComponent<VisionTarget>();
        if (target!=null)
        {
            _targets.Add(target.Id);
            _targetsChanged = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        var target = collision.GetComponent<VisionTarget>();
        if (target != null)
        {
            _targets.Remove(target.Id);
            _targetsChanged = true;
        }
    }

    private void OnDrawGizmos()
    {
        
    }
}
