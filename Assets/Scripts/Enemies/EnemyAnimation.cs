using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private EnemyMovement _enemyMovement;
    [SerializeField] private Health _enemyHealth;


    private void OnEnable()
    {
        _enemyHealth.HealthChanged += OnHealthChanged;
        _enemyMovement.MovementStateChanged += OnMovementStateChanged;
        OnMovementStateChanged(_enemyMovement.IsMoving);
    }

    private void OnDisable()
    {
        _enemyMovement.MovementStateChanged -= OnMovementStateChanged;
        _enemyHealth.HealthChanged -= OnHealthChanged;
    }

    private void OnMovementStateChanged(bool isMoving)
    {
        _animator.SetBool("IsMoving", isMoving);
    }

    private void OnHealthChanged(float currentHealth)
    {
        if (currentHealth <= 0)
            return;

        _animator.SetTrigger("Hit");
    }
}
