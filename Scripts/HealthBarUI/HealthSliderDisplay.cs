using UnityEngine;
using UnityEngine.UI;

public class HealthSliderDisplay : HealthDisplay
{
    [SerializeField] protected Slider HealthSlider;

    protected void Awake()
    {
        HealthSlider.interactable = false;
    }

    override public void DisplayHealthPoints(float value, float maxPoints)
    {
        HealthSlider.value = GetClampedValue(value, maxPoints);
    }

    override public void Deactivate()
    {
        HealthSlider.gameObject.SetActive(false);
    }

    protected float GetClampedValue(float value, float maxPoints)
    {
        float minSliderValue = 0;
        float maxSliderValue = 1;

        return Mathf.Clamp(value / maxPoints, minSliderValue, maxSliderValue);
    }
}
