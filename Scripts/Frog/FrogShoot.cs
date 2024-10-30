using UnityEngine;

public class FrogShoot : MonoBehaviour
{
    [SerializeField] private Transform _shootingPoint;
    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField] private FrogMovement _frogMovement;
    [SerializeField] private float _bulletSpeed = 10f;
    [SerializeField] private float _shootDistance = 5f;

    private bool _isShooting = false;
    private int _leftMouseButtonCode = 0;

    private void Update()
    {
        if (Input.GetMouseButtonDown(_leftMouseButtonCode))
            _isShooting = true;
    }

    private void FixedUpdate()
    {
        if (_isShooting)
        {
            Shoot(GetTargetDirection(DetectEnemy()));
            _isShooting = false;
        }
    }

    private EnemyBehaviour DetectEnemy()
    {
        float distance = 0;
        float closestDistance = Mathf.Infinity;
        EnemyBehaviour closestEnemy = null;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(_shootingPoint.position, _shootDistance);

        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out EnemyBehaviour enemy))
            {
                distance = Vector2.Distance(_shootingPoint.position, enemy.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy;
                }
            }
        }

        return closestEnemy;
    }

    private Vector2 GetTargetDirection(EnemyBehaviour closestEnemy)
    {
        Vector2 targetDirection;
        Vector2 rightDirection = new Vector2(0.8f, 0.8f);
        Vector2 leftDirection = new Vector2(-0.8f, 0.8f);

        if (_frogMovement.IsFacingRight)
            targetDirection = rightDirection;
        else
            targetDirection = leftDirection;

        if (closestEnemy != null)
        {
            Vector3 targetPosition = closestEnemy.transform.position;
            targetDirection = (targetPosition - _shootingPoint.position).normalized;
        }

        return targetDirection;
    }

    private void Shoot(Vector2 targetDirection)
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
