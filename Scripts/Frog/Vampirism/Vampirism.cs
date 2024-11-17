using System.Collections;
using UnityEngine;
using System;

public class Vampirism : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private EnemyDetector _enemyDetector;
    [SerializeField] private float _abilityDuration = 6f;
    [SerializeField] private float _cooldownDuration = 4f;
    [SerializeField] private float _range = 5f;
    [SerializeField] private float _tickDamage = 4f;
    [SerializeField] private float _timeBetweenTicks = 1f;

    public event Action<float> AbilityProgressStarted;
    public event Action<float> CooldownProgressStarted;
    public event Action VisualEffectStarted;
    public event Action VisualEffectEnded;

    public bool IsReady { get; private set; } = true;

    public void StartDrainHealthCoroutine()
    {
        StartCoroutine(DrainHealth());
    }

    private void DrainHealthOneTick()
    {
        EnemyBehaviour enemy = _enemyDetector.GetClosestEnemy(_range, transform.position);

        if (enemy != null)
        {
            float actualDamage = _tickDamage;

            if (enemy.HealthPoints < _tickDamage)
                actualDamage = enemy.HealthPoints;

            enemy.LoosePoints(actualDamage);
            _health.AddPoints(actualDamage);
        }
    }

    private IEnumerator DrainHealth()
    {
        IsReady = false;
        float passedTime = 0;
        WaitForSeconds waitBetweenTicks = new WaitForSeconds(_timeBetweenTicks);

        AbilityProgressStarted?.Invoke(_abilityDuration);
        VisualEffectStarted?.Invoke();

        while (passedTime < _abilityDuration)
        {
            DrainHealthOneTick();
            passedTime += _timeBetweenTicks;
            yield return waitBetweenTicks;
        }

        VisualEffectEnded?.Invoke();
        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        CooldownProgressStarted?.Invoke(_cooldownDuration);
        yield return new WaitForSeconds(_cooldownDuration);
        IsReady = true;
    }
}
