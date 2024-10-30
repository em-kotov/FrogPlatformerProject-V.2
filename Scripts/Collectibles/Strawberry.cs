using UnityEngine;

public class Strawberry : Collectable
{
    private readonly string _commandCollected = "Collected";

    [SerializeField] private Animator _animator;

    public bool CanCollect { get; private set; } = true;

    public void DeactivateWithEffect()
    {
        SetCollected();
        SetAnimation();
    }

    private void SetCollected()
    {
        CanCollect = false;
    }

    private void SetAnimation()
    {
        _animator.SetTrigger(_commandCollected);
    }
}
