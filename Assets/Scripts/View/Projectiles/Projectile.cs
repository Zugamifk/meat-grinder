using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Identifiable _identifiable;

    private void Awake()
    {
        _identifiable = GetComponent<Identifiable>();
    }

    void Update()
    {
        var projectile = Game.Model.Projectiles.GetItem(_identifiable.Id);
        transform.position = projectile.Position;
    }
}
