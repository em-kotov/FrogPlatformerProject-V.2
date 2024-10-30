using System;
using UnityEngine;

public class EnemyHealthCollisionRegister : MonoBehaviour
{
    public event Action HitRegistered;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Bullet bullet))
        {
            HitRegistered?.Invoke();
        }
    }
}
