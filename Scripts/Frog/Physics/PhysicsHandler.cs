using System;
using UnityEngine;

public class PhysicsHandler : MonoBehaviour
{
    [SerializeField] private Mover _mover;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private GroundDetector _groundDetector;

    public event Action<float> SpeedChanged;
    public event Action<bool> IsGroundedChanged;

    private void FixedUpdate()
    {
        Run();
        Jump();
    }

    public EnemyBehaviour GetClosestEnemy(float range, Vector2 position)
    {
        float distance = 0;
        float closestDistance = Mathf.Infinity;
        EnemyBehaviour closestEnemy = null;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, range);

        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out EnemyBehaviour enemy))
            {
                distance = Vector2.Distance(position, enemy.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy;
                }
            }
        }

        return closestEnemy;
    }

    public bool IsFacingRight()
    {
        return _mover.IsFacingRight;
    }

    private void Run()
    {
        _mover.Run(_inputReader.Direction);
        SpeedChanged?.Invoke(_mover.GetSpeed());
    }

    private void Jump()
    {
        if (_inputReader.IsJump() && _groundDetector.IsGrounded())
            _mover.Jump();

        IsGroundedChanged?.Invoke(_groundDetector.IsGrounded());
    }
}
