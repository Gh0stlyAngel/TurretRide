using System;
using UnityEngine;
[SelectionBase]
public class Health : MonoBehaviour
{
    public event Action Died;
    public event Action<float> HealthChanged;

    [SerializeField] private float _maxHealth;
    private float _currentHealth;

    public float MaxHealth => _maxHealth;
    public float CurrentHealth => _currentHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (_currentHealth <= 0 || damage <= 0)
            return;

        _currentHealth = Mathf.Max(0, _currentHealth - damage);
        HealthChanged?.Invoke(_currentHealth);

        if (_currentHealth <= 0)
        {
            Died?.Invoke();
        }
    }

    public void Kill()
    {
        TakeDamage(_currentHealth);
    }
}
