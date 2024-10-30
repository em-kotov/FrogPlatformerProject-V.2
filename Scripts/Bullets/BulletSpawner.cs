using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private int _poolCapacity = 50;
    [SerializeField] private int _poolMaxSize = 50;

    private ObjectPool<Bullet> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Bullet>(
            createFunc: () => Instantiate(_bulletPrefab),
            actionOnGet: (bullet) => Activate(bullet),
            actionOnRelease: (bullet) => Deactivate(bullet),
            actionOnDestroy: (bullet) => Destroy(bullet),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    public Bullet Spawn()
    {
        return _pool.Get();
    }

    public IEnumerator StartDeactivation(Bullet bullet)
    {
        float lifeTime = 0.4f;
        WaitForSeconds wait = new WaitForSeconds(lifeTime);

        while (bullet.IsCollided == false)
            yield return wait;

        _pool.Release(bullet);
    }

    private void Activate(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void Deactivate(Bullet bullet)
    {
        bullet.Reset();
        bullet.gameObject.SetActive(false);
    }
}
