using System;
using UnityEngine;

public class Fist : MonoBehaviour
{
    public CharacterReal Character;
    public float Force = 1;

    private void OnTriggerEnter(Collider other)
    {
        var entity = other.GetComponent<Entity2D>();
        entity.CharAttack(Character);
    }

    private void OnCollisionEnter(Collision other)
    {
        var entity = other.collider.GetComponent<ActionPoint>();
        if(!entity) return;
        entity.CharAttack(Character);
        var pt = other.GetContact(0);
        var rb = other.rigidbody;
        var dir = pt.point - Character.Rigidbody.position;
        dir.z = 0;
        if (rb) rb.AddForceAtPosition(dir.normalized * Force, pt.point);
        
    }
}