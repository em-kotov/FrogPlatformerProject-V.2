using UnityEngine;

public class Subscriber : MonoBehaviour
{
    [SerializeField] private CollisionRegister _collisionRegister;
    [SerializeField] private Health _health;
    [SerializeField] private Frog _frog;
    [SerializeField] private FrogAnimator _animator;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private DisplayInventory _displayInventory;
    [SerializeField] private Vampirism _vampirism;
    [SerializeField] private ManaSlider _manaSlider;
    [SerializeField] private VampirismRenderer _vampirismRenderer;

    private void OnEnable()
    {
        _collisionRegister.HitRegistered += _frog.HandleHit;
        _collisionRegister.HealRegistered += _frog.HandleHeal;

        _collisionRegister.MedkitFound += _inventory.OnMedkitFound;
        _collisionRegister.StrawberryFound += _inventory.OnStrawberryFound;

        _inventory.StrawberryValueChanged += _displayInventory.DisplayStrawberryValue;

        _health.LostPoints += _animator.PlayHitAnimation;
        _health.HasDied += _animator.SetDeathAnimation;
        _frog.SpeedChanged += _animator.UpdateSpeed;
        _frog.IsGroundedChanged += _animator.UpdateJump;

        _health.HasDied += _manaSlider.Deactivate;
        _vampirism.AbilityProgressStarted += _manaSlider.OnAbilityActivated;
        _vampirism.CooldownProgressStarted += _manaSlider.OnCooldownActivated;

        _vampirism.VisualEffectStarted += _vampirismRenderer.OnAbilityStarted;
        _vampirism.VisualEffectEnded += _vampirismRenderer.OnAbilityEnded;
    }

    private void OnDisable()
    {
        _collisionRegister.HitRegistered -= _frog.HandleHit;
        _collisionRegister.HealRegistered -= _frog.HandleHeal;

        _collisionRegister.MedkitFound -= _inventory.OnMedkitFound;
        _collisionRegister.StrawberryFound -= _inventory.OnStrawberryFound;

        _inventory.StrawberryValueChanged -= _displayInventory.DisplayStrawberryValue;

        _health.LostPoints -= _animator.PlayHitAnimation;
        _health.HasDied -= _animator.SetDeathAnimation;
        _frog.SpeedChanged -= _animator.UpdateSpeed;
        _frog.IsGroundedChanged -= _animator.UpdateJump;

        _health.HasDied -= _manaSlider.Deactivate;
        _vampirism.AbilityProgressStarted -= _manaSlider.OnAbilityActivated;
        _vampirism.CooldownProgressStarted -= _manaSlider.OnCooldownActivated;

        _vampirism.VisualEffectStarted -= _vampirismRenderer.OnAbilityStarted;
        _vampirism.VisualEffectEnded -= _vampirismRenderer.OnAbilityEnded;
    }
}
