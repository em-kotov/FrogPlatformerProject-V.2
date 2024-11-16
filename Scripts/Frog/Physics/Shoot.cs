using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private Transform _shootingPoint;
    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField] private float _bulletSpeed = 10f;
    [SerializeField] private float _shootRange = 5f;

    public float ShootRange => _shootRange;
    public Vector2 ShootingPoint => _shootingPoint.position;

    public void MakeOneShot(Vector2 targetDirection)
    {
        Bullet bullet = _bulletSpawner.Spawn();

        if (bullet.TryGetComponent(out Rigidbody2D bulletRigidbody))
        {
            bulletRigidbody.position = _shootingPoint.position;
            bulletRigidbody.AddForce(targetDirection * _bulletSpeed, ForceMode2D.Impulse);
        }

        StartCoroutine(_bulletSpawner.StartDeactivation(bullet));
    }

    public Vector2 GetShootingDirection(EnemyBehaviour closestEnemy, bool isFacingRight)
    {
        Vector2 shootingDirection;
        Vector2 rightDirection = new Vector2(0.8f, 0.8f);
        Vector2 leftDirection = new Vector2(-0.8f, 0.8f);

        if (isFacingRight)
            shootingDirection = rightDirection;
        else
            shootingDirection = leftDirection;

        if (closestEnemy != null)
        {
            Vector3 targetPosition = closestEnemy.transform.position;
            shootingDirection = (targetPosition - _shootingPoint.position).normalized;
        }

        return shootingDirection;
    }
}
