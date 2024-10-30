using UnityEngine;

public class MedkitSpawner : MonoBehaviour
{
    [SerializeField] private Medkit _medkitPrefab;
    [SerializeField] private float _radius = 3f;
    [SerializeField] private int _count = 1;

    private void Awake()
    {
        for (int i = 0; i < _count; i++)
            Create();
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 randomPosition = Random.insideUnitSphere * _radius;
        return randomPosition + transform.position;
    }

    private void Create()
    {
        Instantiate(_medkitPrefab, GetRandomPosition(), Quaternion.identity);
    }
}
