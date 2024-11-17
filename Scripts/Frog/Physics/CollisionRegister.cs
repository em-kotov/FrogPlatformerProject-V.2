using System;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class CollisionRegister : MonoBehaviour
{
    public event Action HitRegistered;
    public event Action HealRegistered;
    public event Action<Medkit> MedkitFound;
    public event Action<Strawberry> StrawberryFound;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out EnemyBehaviour enemy))
            HandleEnemy();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Collectable collectable))
        {
            if (collectable is Medkit medkit)
                HandleMedkit(medkit);

            if (collectable is Strawberry strawberry)
                HandleStrawberry(strawberry);
        }
    }

    private void HandleEnemy()
    {
        HitRegistered?.Invoke();
    }

    private void HandleMedkit(Medkit medkit)
    {
        MedkitFound?.Invoke(medkit);
        HealRegistered?.Invoke();
    }

    private void HandleStrawberry(Strawberry strawberry)
    {
        StrawberryFound?.Invoke(strawberry);
    }
}
