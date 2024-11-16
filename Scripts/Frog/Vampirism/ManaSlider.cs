using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ManaSlider : MonoBehaviour
{
    [SerializeField] private Vampirism _vampirism;
    [SerializeField] private Slider _slider;
    [SerializeField] private Health _health;

    private Coroutine _smoothValueMoveCoroutine;
    private float _manaFull = 1f;
    private float _manaEmpty = 0f;

    private void Awake()
    {
        _slider.interactable = false;
        _slider.value = _manaFull;
    }

    private void OnEnable()
    {
        _vampirism.AbilityProgressStarted += OnAbilityActivated;
        _vampirism.CooldownProgressStarted += OnCooldownActivated;
        _health.HasDied += Deactivate;
    }

    private void OnDisable()
    {
        _vampirism.AbilityProgressStarted -= OnAbilityActivated;
        _vampirism.CooldownProgressStarted -= OnCooldownActivated;
        _health.HasDied -= Deactivate;
    }

    private void OnAbilityActivated(float duration)
    {
        ActivateSmoothValueMove(_manaFull, _manaEmpty, duration);
    }

    private void OnCooldownActivated(float duration)
    {
        ActivateSmoothValueMove(_manaEmpty, _manaFull, duration);
    }

    private void Deactivate()
    {
        if (_smoothValueMoveCoroutine != null)
            StopCoroutine(_smoothValueMoveCoroutine);

        _slider.gameObject.SetActive(false);
    }

    private void ActivateSmoothValueMove(float startValue, float targetValue, float duration)
    {
        if (_smoothValueMoveCoroutine != null)
            StopCoroutine(_smoothValueMoveCoroutine);

        _smoothValueMoveCoroutine = StartCoroutine(SmoothValueMove(startValue, targetValue, duration));
    }

    private IEnumerator SmoothValueMove(float startValue, float targetValue, float duration)
    {
        float passedTime = 0f;
        float timeProgress;

        while (passedTime < duration)
        {
            passedTime += Time.deltaTime;
            timeProgress = Mathf.Clamp01(passedTime / duration);
            _slider.value = Mathf.Lerp(startValue, targetValue, timeProgress);
            yield return null;
        }

        _slider.value = targetValue;
        _smoothValueMoveCoroutine = null;
    }
}
