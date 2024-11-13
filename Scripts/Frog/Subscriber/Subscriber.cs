using UnityEngine;

public class Subscriber : MonoBehaviour
{
    [SerializeField] private HealthCollisionRegister _healthCollisionRegister;
    [SerializeField] private Health _health;
    [SerializeField] private PhysicsHandler _physicsHandler;
    [SerializeField] private FrogAnimator _animator;
    [SerializeField, Range(0f, 100f)] private float _addedPoints = 10;
    [SerializeField, Range(0f, 100f)] private float _lostPoints = 10;

    private void OnEnable()
    {
        _healthCollisionRegister.HitRegistered += HandleHit;
        _healthCollisionRegister.HealRegistered += HandleHeal;
        _health.LostPoints += _animator.PlayHitAnimation;
        _health.HasDied += _animator.SetDeathAnimation;
        _physicsHandler.SpeedChanged += _animator.UpdateSpeed;
        _physicsHandler.IsGroundedChanged += _animator.UpdateJump;
    }

    private void OnDisable()
    {
        _healthCollisionRegister.HitRegistered -= HandleHit;
        _healthCollisionRegister.HealRegistered -= HandleHeal;
        _health.LostPoints -= _animator.PlayHitAnimation;
        _health.HasDied -= _animator.SetDeathAnimation;
        _physicsHandler.SpeedChanged -= _animator.UpdateSpeed;
        _physicsHandler.IsGroundedChanged -= _animator.UpdateJump;
    }

    private void HandleHit()
    {
        _health.LoosePoints(_lostPoints);
    }

    private void HandleHeal()
    {
        _health.AddPoints(_addedPoints);
    }
}
