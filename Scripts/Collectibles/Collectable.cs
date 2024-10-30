using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Collectable : MonoBehaviour
{
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
