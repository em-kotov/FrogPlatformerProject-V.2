using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    public EnemyBehaviour GetClosestEnemy(float range, Vector3 position)
    {
        float sqrDistance;
        float sqrClosestDistance = 50f * 50f;
        int enemyLayerIndex = 7;
        EnemyBehaviour closestEnemy = null;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, range,
                                        LayerMaskConverter.GetLayerMask(enemyLayerIndex));

        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out EnemyBehaviour enemy))
            {
                sqrDistance = Vector3Extensions.GetSqrDistance(position, enemy.transform.position);

                if (sqrDistance <= sqrClosestDistance)
                {
                    sqrClosestDistance = sqrDistance;
                    closestEnemy = enemy;
                }
            }
        }

        return closestEnemy;
    }
}
