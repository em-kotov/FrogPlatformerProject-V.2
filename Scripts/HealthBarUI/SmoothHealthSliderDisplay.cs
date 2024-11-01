using UnityEngine;
using System.Collections;

public class SmoothHealthSliderDisplay : HealthSliderDisplay
{
    private Coroutine _smoothValueMoveCoroutine;
    private bool _isMoving = false;

    override public void DisplayHealthPoints(float value, float maxPoints)
    {
        ActivateSmoothValueMove(GetClampedValue(value, maxPoints));
    }

    override public void Deactivate()
    {
        StartCoroutine(DeactivateAfterMove());
    }

    private IEnumerator DeactivateAfterMove()
    {
        while (_isMoving)
            yield return null;

        base.Deactivate();
    }

    private void ActivateSmoothValueMove(float targetValue)
    {
        if (_smoothValueMoveCoroutine != null)
            StopCoroutine(_smoothValueMoveCoroutine);

        _smoothValueMoveCoroutine = StartCoroutine(SmoothValueMove(HealthSlider.value, targetValue));
    }

    private IEnumerator SmoothValueMove(float startValue, float targetValue)
    {
        _isMoving = true;
        float passedTime = 0f;
        float targetTime = 0.6f;
        float clampedTime;

        while (passedTime < targetTime)
        {
            passedTime += Time.deltaTime;
            clampedTime = Mathf.Clamp01(passedTime / targetTime);
            HealthSlider.value = Mathf.Lerp(startValue, targetValue, clampedTime);
            yield return null;
        }

        HealthSlider.value = targetValue;
        _isMoving = false;
        _smoothValueMoveCoroutine = null;
    }
}
