using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _runSpeed = 4.9f;
    [SerializeField] private float _jumpForce = 7f;

    private Rigidbody2D _rigidbody;

    public bool IsFacingRight { get; private set; } = true;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Run(float direction)
    {
        _rigidbody.linearVelocity = new Vector2(direction * _runSpeed, _rigidbody.linearVelocity.y);
        UpdateFlip(direction);
    }

    public void Jump()
    {
        _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, _jumpForce);
    }

    public float GetSpeed()
    {
        return Mathf.Abs(_rigidbody.linearVelocity.x);
    }

    private void UpdateFlip(float direction)
    {
        float minInput = 0.1f;

        if (direction > minInput && !IsFacingRight)
        {
            Flip();
            IsFacingRight = true;
        }

        if (direction < -minInput && IsFacingRight)
        {
            Flip();
            IsFacingRight = false;
        }
    }

    private void Flip()
    {
        IsFacingRight = !IsFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
