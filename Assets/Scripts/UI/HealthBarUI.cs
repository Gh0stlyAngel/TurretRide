using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Health _carHealth;
    private Slider _healthBar;

    private void Awake()
    {
        _healthBar = GetComponent<Slider>();
    }

    private void Start()
    {
        _healthBar.minValue = 0f;
        _healthBar.maxValue = _carHealth.MaxHealth;
        _healthBar.value = _carHealth.CurrentHealth;
    }
    private void OnEnable()
    {
        _carHealth.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _carHealth.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(float currentHealth)
    {
        _healthBar.value = currentHealth;
    }
}
