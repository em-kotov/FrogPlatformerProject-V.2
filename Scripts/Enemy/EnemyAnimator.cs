using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private readonly string _commandIsDead = "IsDead";
    
    [SerializeField] private Animator _animator;

    public void SetDeathAnimation()
    {
        _animator.SetBool(_commandIsDead, true);
    }

    private void Deactivate() //used inside animation as event (death animation)
    {
        gameObject.SetActive(false);
    }
}
