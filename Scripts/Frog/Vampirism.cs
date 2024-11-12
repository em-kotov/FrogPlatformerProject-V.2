using System.Collections;
using UnityEngine;
using System;

public class Vampirism : MonoBehaviour
{
    [SerializeField] private Health _frogHealth;
    [SerializeField] private SpriteRenderer _abilityRenderer;

    private const KeyCode _abilityKey = KeyCode.Q;

    private float _abilityDuration = 6f;
    private float _cooldownDuration = 4f;
    private float _abilityRange = 5f;
    private bool _abilityIsReady = true;

    public event Action<float> AbilityActivated;
    public event Action<float> CooldownActivated;

    private void Start()
    {
        DrawAbility(false);
    }

    private void Update()
    {
        if (_abilityIsReady && Input.GetKeyDown(_abilityKey))
            Activate();
    }

    private void DrawAbility(bool IsVisible)
    {
        _abilityRenderer.enabled = IsVisible;
    }

    private void Activate()
    {
        StartCoroutine(DrainHealth());
    }

    private void DrainHealthOneTick()
    {
        float tick = 4f;
        Health enemyHealth = DetectEnemy(_abilityRange, transform.position);

        if (enemyHealth != null)
        {
            enemyHealth.LoosePoints(tick);
            _frogHealth.AddPoints(tick);
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
            DrawAbility(true);
            DrainHealthOneTick();
            yield return wait;
        }

        DrawAbility(false);
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

    private Health DetectEnemy(float range, Vector2 position)
    {
        float distance = 0;
        float closestDistance = Mathf.Infinity;
        Health closestEnemy = null;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, range);

        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out EnemyBehaviour enemy))
            {
                distance = Vector2.Distance(position, enemy.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy.GetComponentInChildren<Health>();
                }
            }
        }

        return closestEnemy;
    }
}
