using System;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public CharacterGame Character;
    public float Force = 1;


    private void OnTriggerEnter2D(Collider2D other)
    {
        var entity = other.GetComponent<Entity2D>();

        entity.CharAttack(Character);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var entity = other.collider.GetComponent<Entity2D>();
        if(!entity) return;
        entity.CharAttack(Character);
        if(!other.otherRigidbody) return;
        var pt = other.GetContact(0);
        var rb = other.rigidbody;
        var dir = pt.point - Character.Rigidbody.position;
        if (rb) rb.AddForceAtPosition(dir.normalized * Force, pt.point);
        
    }
}