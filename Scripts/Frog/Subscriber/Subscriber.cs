using UnityEngine;

public class Subscriber : MonoBehaviour
{
    [SerializeField] private HealthCollisionRegister _healthCollisionRegister;
    [SerializeField] private Health _health;
    [SerializeField] private Frog _frog;
    [SerializeField] private FrogAnimator _animator;

    private void OnEnable()
    {
        _healthCollisionRegister.HitRegistered += _frog.HandleHit;
        _healthCollisionRegister.HealRegistered += _frog.HandleHeal;
        _health.LostPoints += _animator.PlayHitAnimation;
        _health.HasDied += _animator.SetDeathAnimation;
        _frog.SpeedChanged += _animator.UpdateSpeed;
        _frog.IsGroundedChanged += _animator.UpdateJump;
    }

    private void OnDisable()
    {
        _healthCollisionRegister.HitRegistered -= _frog.HandleHit;
        _healthCollisionRegister.HealRegistered -= _frog.HandleHeal;
        _health.LostPoints -= _animator.PlayHitAnimation;
        _health.HasDied -= _animator.SetDeathAnimation;
        _frog.SpeedChanged -= _animator.UpdateSpeed;
        _frog.IsGroundedChanged -= _animator.UpdateJump;
    }
}
