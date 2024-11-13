using System.Collections;
using UnityEngine;
using System;

public class Vampirism : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private PhysicsHandler _physicsHandler;
    [SerializeField] private InputReader _inputReader;

    private float _abilityDuration = 6f;
    private float _cooldownDuration = 4f;
    private float _abilityRange = 5f;
    private bool _abilityIsReady = true;

    public event Action<float> AbilityActivated;
    public event Action<float> CooldownActivated;

    private void FixedUpdate() 
    {
        if (_abilityIsReady && _inputReader.IsAbilityPressed())
            Activate();
    }

    private void Activate()
    {
        StartCoroutine(DrainHealth());
    }

    private void DrainHealthOneTick()
    {
        float tick = 4f;
        EnemyBehaviour enemy = _physicsHandler.GetClosestEnemy(_abilityRange, transform.position);

        if (enemy != null && enemy.IsAlive)
        {
            if (enemy.CanAcceptAllDamage(tick, out float pointsAccepted) == false)
                tick = pointsAccepted;

            enemy.LoosePoints(tick);
            _health.AddPoints(tick);
        }
    }

    private IEnumerator DrainHealth()
    {
        _abilityIsReady = false;
        float timePassed = 0;
        float waitTime = 1f;
        WaitForSeconds wait = new WaitForSeconds(waitTime);

        AbilityActivated?.Invoke(_abilityDuration);

        while (timePassed < _abilityDuration)
        {
            timePassed++;
            DrainHealthOneTick();
            yield return wait;
        }

        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        float timePassed = 0;
        float waitTime = 1f;
        WaitForSeconds wait = new WaitForSeconds(waitTime);

        CooldownActivated?.Invoke(_cooldownDuration);

        while (timePassed < _cooldownDuration)
        {
            timePassed++;
            yield return wait;
        }

        _abilityIsReady = true;
    }
}
