using UnityEngine;

public class FloatFollowCanvas : MonoBehaviour
{
    [SerializeField] private Canvas _worldSpaceCanvas;
    [SerializeField] private Transform _targetToFollow;
    [SerializeField] private Vector3 _offset = new Vector3(0, 1.5f, 0);

    [Header("Optional Settings")]
    [SerializeField] private bool _useSmoothing = false;
    [SerializeField] private float _smoothSpeed = 10f;

    private void LateUpdate()
    {
        FollowCharacter(_targetToFollow.position + _offset);
    }

    private void FollowCharacter(Vector3 targetPosition)
    {
        if (_useSmoothing)
        {
            _worldSpaceCanvas.transform.position = Vector3.Lerp(
                _worldSpaceCanvas.transform.position,
                targetPosition,
                _smoothSpeed * Time.deltaTime
            );
        }
        else
        {
            _worldSpaceCanvas.transform.position = targetPosition;
        }
    }
}
