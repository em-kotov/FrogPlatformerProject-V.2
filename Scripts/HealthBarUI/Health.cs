using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] private float _maxPoints = 100;

    private float _minPoints = 0;

    public event Action<float, float> PointsChanged;
    public event Action HasDied;
    public event Action LostPoints;

    public float Points { get; private set; }

    private void Start()
    {
        SetCurrentHealth(_maxPoints);
    }

    public void LoosePoints(float lostPoints)
    {
        if (IsNegative(lostPoints))
            return;

        LostPoints?.Invoke();
        SetCurrentHealth(GetClampedPoints(Points - lostPoints));
    }

    public void AddPoints(float addedPoints)
    {
        if (IsNegative(addedPoints))
            return;

        SetCurrentHealth(GetClampedPoints(Points + addedPoints));
    }

    private void SetCurrentHealth(float points)
    {
        Points = points;
        InvokePointsChanged();
        InvokeHasDied();
    }

    private float GetClampedPoints(float points)
    {
        return Mathf.Clamp(points, _minPoints, _maxPoints);
    }

    private bool IsNegative(float value)
    {
        return value < 0;
    }

    private void InvokePointsChanged()
    {
        PointsChanged?.Invoke(Points, _maxPoints);
    }

    private void InvokeHasDied()
    {
        if (Points <= 0)
            HasDied?.Invoke();
    }
}
