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
        HealthSlider.value = Mathf.Clamp01(value / maxPoints);
    }

    override public void Deactivate()
    {
        HealthSlider.gameObject.SetActive(false);
    }
}
