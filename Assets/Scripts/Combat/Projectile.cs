using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifetime;
    [SerializeField] private float _damage;
    [SerializeField] private LayerMask _hitMask;

    private SphereCollider _collider;

    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
    }

    private void Start()
    {
        Destroy(gameObject, _lifetime);
    }

    private void Update()
    {
        float distance = _speed * Time.deltaTime;
        Vector3 direction = transform.forward;

        float scale = Mathf.Max(
            Mathf.Abs(transform.lossyScale.x),
            Mathf.Abs(transform.lossyScale.y),
            Mathf.Abs(transform.lossyScale.z)
        );

        float radius = _collider.radius * scale;

        Vector3 origin = transform.TransformPoint(_collider.center);

        if (Physics.SphereCast(
            origin,
            radius,
            direction,
            out RaycastHit hit,
            distance,
            _hitMask,
            QueryTriggerInteraction.Collide))
        {
            if (hit.collider.TryGetComponent<Enemy>(out var enemy))
            {
                enemy.Health.TakeDamage(_damage);
                Destroy(gameObject);
                return;
            }
        }

        transform.position += direction * distance;
    }
}
