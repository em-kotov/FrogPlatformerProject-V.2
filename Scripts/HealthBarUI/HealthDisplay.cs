using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] protected Health Health;

    public virtual void OnEnable()
    {
        Health.PointsChanged += DisplayHealthPoints;
        Health.HasDied += Deactivate;
    }

    public virtual void OnDisable()
    {
        Health.PointsChanged -= DisplayHealthPoints;
        Health.HasDied -= Deactivate;
    }

    public virtual void DisplayHealthPoints(float value, float maxValue) { }

    public virtual void Deactivate() { }
}
