using UnityEngine;

public class EnemySubscriber : MonoBehaviour
{
    [SerializeField] private EnemyHealthCollisionRegister _healthCollisionRegister;
    [SerializeField] private Health _health;
    [SerializeField] private EnemyAnimator _animator;
    [SerializeField, Range(0f, 100f)] private float _lostPoints = 10;

    private void OnEnable()
    {
        _healthCollisionRegister.HitRegistered += HandleHit;
        _health.HasDied += _animator.SetDeathAnimation;
    }

    private void OnDisable()
    {
        _healthCollisionRegister.HitRegistered -= HandleHit;
        _health.HasDied -= _animator.SetDeathAnimation;
    }

    private void HandleHit()
    {
        _health.LoosePoints(_lostPoints);
    }
}
