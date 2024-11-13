using System;
using UnityEngine;

public class HealthCollisionRegister : MonoBehaviour
{
    public event Action HitRegistered;
    public event Action HealRegistered;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out EnemyBehaviour enemy))
            HitRegistered?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Medkit medkit))
        {
            medkit.Deactivate();
            HealRegistered?.Invoke();
        }
    }
}
