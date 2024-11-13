using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;

    private int _groundedLayerNumber = 1;

    public bool IsGrounded()
    {
        float distance = 0.2f;
        RaycastHit2D hit = Physics2D.Raycast(_groundCheck.position, Vector2.down, distance, _groundedLayerNumber);
        return hit.collider != null;
    }
}
