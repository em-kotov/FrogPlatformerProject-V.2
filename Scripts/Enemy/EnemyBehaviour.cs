using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private Transform _waypointMain;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _detectDistance = 3f;
    [SerializeField] private float _chaseDistance = 3f;

    private Transform[] _waypoints;
    private Transform _frog = null;
    private int _waypointIndex = 0;
    private bool _canChase = false;

    private void Start()
    {
        AddWaypoints();
    }

    private void Update()
    {
        DetectFrog();

        if (_canChase)
        {
            ChaseFrog();
            UpdateTarget();
        }
        else
        {
            Patrol();
        }
    }

    private void AddWaypoints()
    {
        _waypoints = new Transform[_waypointMain.childCount];

        for (int i = 0; i < _waypointMain.childCount; i++)
            _waypoints[i] = _waypointMain.GetChild(i);
    }

    private void Patrol()
    {
        Transform currentWaypoint = _waypoints[_waypointIndex];
        transform.position = Vector2.MoveTowards(transform.position, currentWaypoint.position, _speed * Time.deltaTime);

        if (transform.position == currentWaypoint.position)
            CalculateNextWaypointIndex();
    }

    private void CalculateNextWaypointIndex()
    {
        _waypointIndex++;

        if (_waypointIndex == _waypoints.Length)
            _waypointIndex = 0;
    }

    private void DetectFrog()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _detectDistance);

        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out FrogShoot frog))
            {
                _frog = frog.transform;
                _canChase = true;
            }
        }
    }

    private void ChaseFrog()
    {
        transform.position = Vector2.MoveTowards(transform.position, _frog.position, _speed * Time.deltaTime);
    }

    private void UpdateTarget()
    {
        float distance = Vector2.Distance(transform.position, _frog.position);

        if (distance > _chaseDistance)
        {
            _frog = null;
            _canChase = false;
        }
    }
}
