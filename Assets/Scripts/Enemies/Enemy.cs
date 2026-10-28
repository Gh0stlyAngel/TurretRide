using UnityEngine;

[RequireComponent(typeof(Health), typeof(EnemyVFX))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _collisionDamage;
    private bool _hasCollided;
    private EnemyVFX _vfx;

    public EnemyVFX VFX => _vfx;
    public Health Health {  get; private set; }

    private void Awake()
    {
        Health = GetComponent<Health>();
        _vfx = GetComponent<EnemyVFX>();
    }

    private void OnEnable()
    {
        Health.Died += OnDied;
    }

    private void OnDisable()
    {
        Health.Died -= OnDied;
    }

    public void Initialize(Transform target, GameManager gameManager)
    {
        EnemyMovement enemyMovement = GetComponent<EnemyMovement>();
        enemyMovement.Initialize(target, gameManager);

        _vfx.Initialize(target);
    }

    private void OnDied()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_hasCollided || Health.CurrentHealth <= 0)
            return;

        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<Health>(out var carHealth))
            {
                _hasCollided = true; 
                carHealth.TakeDamage(_collisionDamage);
                Health.Kill();
            }
        }
    }
}
