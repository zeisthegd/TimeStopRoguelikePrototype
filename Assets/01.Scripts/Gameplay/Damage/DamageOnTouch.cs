using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageOnTouch : MonoBehaviour
{
    public LayerMask TargetMask;
    public LayerMask ObstacleMask;
    private float damage;

    public event UnityAction CollideWithObstacle;

    void DealDamage(GameObject gObject)
    {
        Character character = gObject.FindComponent<Character>();
        character.Health?.Take(damage);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (TargetMask.Contains(col.gameObject.layer))
        {
            DealDamage(col.gameObject);
        }
        else if (ObstacleMask.Contains(col.gameObject.layer))
        {
            CollideWithObstacle?.Invoke();
        }
    }

    public float Damage { get => damage; set => damage = value; }
}
