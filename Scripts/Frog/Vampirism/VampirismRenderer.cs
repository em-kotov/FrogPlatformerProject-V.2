using UnityEngine;

public class VampirismRenderer : MonoBehaviour
{
    [SerializeField] private Vampirism _vampirism;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        DrawAbility(false);
    }

    private void OnEnable()
    {
        _vampirism.AbilityEffectStarted += OnAbilityStarted;
        _vampirism.AbilityEffectEnded += OnAbilityEnded;
    }

    private void OnDisable()
    {
        _vampirism.AbilityEffectStarted -= OnAbilityStarted;
        _vampirism.AbilityEffectEnded -= OnAbilityEnded;
    }

    private void OnAbilityStarted()
    {
        DrawAbility(true);
    }

    private void OnAbilityEnded()
    {
        DrawAbility(false);
    }

    private void DrawAbility(bool isVisible)
    {
        _spriteRenderer.enabled = isVisible;
    }
}
