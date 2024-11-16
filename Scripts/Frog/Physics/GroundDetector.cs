using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;

    public bool IsGrounded()
    {
        int groundLayerIndex = 9;
        int enemyLayerIndex = 7;
        float distance = 0.2f;
        RaycastHit2D hit = Physics2D.Raycast(_groundCheck.position, Vector2.down, distance,
                                LayerMaskConverter.GetLayerMask(groundLayerIndex, enemyLayerIndex));
        return hit.collider != null;
    }
}
