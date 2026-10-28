using UnityEngine;
using System.Collections;

public class EnemyVFX : MonoBehaviour
{
    [SerializeField] private Health _enemyHealth;
    [SerializeField] private SkinnedMeshRenderer _renderer;
    [SerializeField] private Material _hitMaterial;
    [SerializeField] private float _hitFlashDuration;
    [SerializeField] private Transform _hitVFXPoint;
    [SerializeField] private ParticleSystem _hitParticlesPrefab;
    [SerializeField] private ParticleSystem _deathParticlesPrefab;

    private Material _originalMaterial;
    private Coroutine _hitFlashCoroutine;
    private Transform _target;

    private void Awake()
    {
        _originalMaterial = _renderer.sharedMaterial;
    }

    private void OnEnable()
    {
        _enemyHealth.Died += OnDeath;
        _enemyHealth.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _enemyHealth.Died -= OnDeath;
        _enemyHealth.HealthChanged -= OnHealthChanged;

        if (_hitFlashCoroutine != null)
        {
            StopCoroutine(_hitFlashCoroutine);
            _hitFlashCoroutine = null;
        }

        _renderer.sharedMaterial = _originalMaterial;
    }

    public void Initialize(Transform target)
    {
        _target = target;
    }

    public void PlayHitParticles()
    {
        Instantiate(
            _hitParticlesPrefab,
            _hitVFXPoint.position,
            GetParticlesRotation()
        );
    }

    private Quaternion GetParticlesRotation()
    {
        Vector3 direction = transform.position - _target.position;
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.001f)
            direction = -transform.forward;

        return Quaternion.LookRotation(direction);
    }

    private void OnHealthChanged(float currentHealth)
    {
        if (currentHealth <= 0)
            return;

        PlayHitParticles();

        if (_hitFlashCoroutine != null)
            StopCoroutine(_hitFlashCoroutine);

        _hitFlashCoroutine = StartCoroutine(ApplyHitFlash());
    }

    private IEnumerator ApplyHitFlash()
    {
        _renderer.sharedMaterial = _hitMaterial;
        yield return new WaitForSeconds(_hitFlashDuration);
        _renderer.sharedMaterial = _originalMaterial;

        _hitFlashCoroutine = null;
    }

    private void OnDeath()
    {
        Instantiate(_deathParticlesPrefab, _hitVFXPoint.position, GetParticlesRotation());
    }
}
