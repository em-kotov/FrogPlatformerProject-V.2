using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D), typeof(SpriteRenderer))]
public class FrogAnimator : MonoBehaviour
{
    private readonly string _commandSpeed = "Speed";
    private readonly string _commandIsGrounded = "IsGrounded";
    private readonly string _commandWasHit = "WasHit";
    private readonly string _commandIsDead = "IsDead";

    [SerializeField] private Animator _animator;

    public void UpdateSpeed(float speed)
    {
        _animator.SetFloat(_commandSpeed, speed);
    }

    public void UpdateJump(bool isGrounded)
    {
        _animator.SetBool(_commandIsGrounded, isGrounded);
    }

    public void PlayHitAnimation()
    {
        _animator.SetTrigger(_commandWasHit);
    }

    public void SetDeathAnimation()
    {
        _animator.SetBool(_commandIsDead, true);
    }

    private void Disappear() //used inside animation as event (death animation)
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
