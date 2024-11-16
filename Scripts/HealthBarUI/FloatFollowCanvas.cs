using UnityEngine;

public class FloatFollowCanvas : MonoBehaviour
{
    [SerializeField] private Canvas _worldSpaceCanvas;
    [SerializeField] private Transform _targetToFollow;
    [SerializeField] private Vector3 _offset = new Vector3(0, 1.5f, 0);

    private void LateUpdate()
    {
        FollowCharacter(_targetToFollow.position + _offset);
    }

    private void FollowCharacter(Vector3 targetPosition)
    {
        _worldSpaceCanvas.transform.position = targetPosition;
    }
}
