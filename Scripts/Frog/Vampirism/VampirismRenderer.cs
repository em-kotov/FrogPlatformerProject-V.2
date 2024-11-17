using UnityEngine;

public class VampirismRenderer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        DrawAbility(false);
    }

    public void OnAbilityStarted()
    {
        DrawAbility(true);
    }

    public void OnAbilityEnded()
    {
        DrawAbility(false);
    }

    private void DrawAbility(bool isVisible)
    {
        _spriteRenderer.enabled = isVisible;
    }
}
