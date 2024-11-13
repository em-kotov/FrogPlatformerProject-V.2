using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] private float _maxPoints = 100;

    private float _points;
    private float _minPoints = 0;

    public event Action<float, float> PointsChanged;
    public event Action HasDied;
    public event Action LostPoints;

    public float Points => _points;

    private void Start()
    {
        SetCurrentHealth(_maxPoints);
    }

    public void LoosePoints(float lostPoints)
    {
        if (IsNegative(lostPoints))
            return;

        LostPoints?.Invoke();
        SetCurrentHealth(GetClampedPoints(_points - lostPoints));
    }

    public void AddPoints(float addedPoints)
    {
        if (IsNegative(addedPoints))
            return;

        SetCurrentHealth(GetClampedPoints(_points + addedPoints));
    }

    public bool HasPoints()
    {
        return _points > 0;
    }

    // public bool CanAcceptAllDamage(float pointsToLoose, out float pointsAccepted)
    // {
    //     pointsAccepted = _points;
    //     return _points >= pointsToLoose;
    // }

    private void SetCurrentHealth(float points)
    {
        _points = points;
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
        PointsChanged?.Invoke(_points, _maxPoints);
    }

    private void InvokeHasDied()
    {
        if (HasPoints() == false)
            HasDied?.Invoke();
    }
}
