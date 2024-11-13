using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    public bool IsCollided { get; private set; } = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Shoot frog) == false)
            IsCollided = true;
    }

    public void Reset()
    {
        IsCollided = false;
    }
}
