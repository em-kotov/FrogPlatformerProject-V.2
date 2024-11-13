using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VampirismRenderer : MonoBehaviour
{
    [SerializeField] private Vampirism _vampirism;
    [SerializeField] private Slider _slider;
    [SerializeField] private Health _health;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private Coroutine _smoothValueMoveCoroutine;
    private float _emptySliderValue = 0f;
    private float _fullSliderValue = 1f;

    private void Awake()
    {
        _slider.interactable = false;
        _slider.value = _fullSliderValue;
        DrawAbility(false);
    }

    private void OnEnable()
    {
        _vampirism.AbilityActivated += HandleActivation;
        _vampirism.CooldownActivated += HandleCooldown;
        _health.HasDied += Deactivate;
    }

    private void OnDisable()
    {
        _vampirism.AbilityActivated -= HandleActivation;
        _vampirism.CooldownActivated -= HandleCooldown;
        _health.HasDied -= Deactivate;
    }

    private void HandleCooldown(float duration)
    {
        DrawAbility(false);
        ActivateSmoothValueMove(_emptySliderValue, _fullSliderValue, duration);
    }

    private void HandleActivation(float duration)
    {
        DrawAbility(true);
        ActivateSmoothValueMove(_fullSliderValue, _emptySliderValue, duration);
    }

    private void Deactivate()
    {
        _slider.gameObject.SetActive(false);
    }

    private void DrawAbility(bool isVisible)
    {
        _spriteRenderer.enabled = isVisible;
    }

    private void ActivateSmoothValueMove(float startValue, float targetValue, float targetTime)
    {
        if (_smoothValueMoveCoroutine != null)
            StopCoroutine(_smoothValueMoveCoroutine);

        _smoothValueMoveCoroutine = StartCoroutine(SmoothValueMove(startValue, targetValue, targetTime));
    }

    private IEnumerator SmoothValueMove(float startValue, float targetValue, float targetTime)
    {
        float passedTime = 0f;
        float clampedTime;

        while (passedTime < targetTime)
        {
            passedTime += Time.deltaTime;
            clampedTime = Mathf.Clamp01(passedTime / targetTime);
            _slider.value = Mathf.Lerp(startValue, targetValue, clampedTime);
            yield return null;
        }

        _slider.value = targetValue;
        _smoothValueMoveCoroutine = null;
    }
}