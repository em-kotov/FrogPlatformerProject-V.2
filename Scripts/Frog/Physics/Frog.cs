using System;
using UnityEngine;

public class Frog : MonoBehaviour
{
    [SerializeField] private Mover _mover;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private EnemyDetector _enemyDetector;
    [SerializeField] private Shoot _shoot;
    [SerializeField] private Vampirism _vampirism;
    [SerializeField] private Health _health;
    [SerializeField, Range(0f, 100f)] private float _addedPoints = 10;
    [SerializeField, Range(0f, 100f)] private float _lostPoints = 10;

    public event Action<float> SpeedChanged;
    public event Action<bool> IsGroundedChanged;

    private void FixedUpdate()
    {
        Run();
        Jump();
        Shoot();
        ActivateVampirism();
    }

    public void HandleHit()
    {
        _health.LoosePoints(_lostPoints);
    }

    public void HandleHeal()
    {
        _health.AddPoints(_addedPoints);
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

    private void Shoot()
    {
        if (_inputReader.IsShooting())
            _shoot.MakeOneShot(_shoot.GetShootingDirection(
                        _enemyDetector.GetClosestEnemy(_shoot.ShootRange, _shoot.ShootingPoint),
                        _mover.IsFacingRight)
                        );
    }

    private void ActivateVampirism()
    {
        if (_inputReader.IsAbilityPressed() && _vampirism.IsReady)
            _vampirism.StartDrainHealthCoroutine();
    }
}
