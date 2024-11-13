using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private Transform _shootingPoint;
    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField] private PhysicsHandler _physicsHandler;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private float _bulletSpeed = 10f;
    [SerializeField] private float _shootRange = 5f;

    private void FixedUpdate()
    {
        if (_inputReader.IsShooting())
            MakeOneShot(GetShootingDirection(_physicsHandler.GetClosestEnemy(_shootRange, _shootingPoint.position)));
    }

    private Vector2 GetShootingDirection(EnemyBehaviour closestEnemy)
    {
        Vector2 shootingDirection;
        Vector2 rightDirection = new Vector2(0.8f, 0.8f);
        Vector2 leftDirection = new Vector2(-0.8f, 0.8f);

        if (_physicsHandler.IsFacingRight())
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

    private void MakeOneShot(Vector2 targetDirection)
    {
        Bullet bullet = _bulletSpawner.Spawn();

        if (bullet.TryGetComponent(out Rigidbody2D bulletRigidbody))
        {
            bulletRigidbody.position = _shootingPoint.position;
            bulletRigidbody.AddForce(targetDirection * _bulletSpeed, ForceMode2D.Impulse);
        }

        StartCoroutine(_bulletSpawner.StartDeactivation(bullet));
    }
}
