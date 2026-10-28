using System.Collections;
using UnityEngine;

public class TurretShooting : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _fireInterval;
    private Coroutine _shooter;

    private IEnumerator Shooter()
    {
        while (true)
        {
            Instantiate(_projectilePrefab, _firePoint.position, _firePoint.rotation);
            yield return new WaitForSeconds(_fireInterval);
        }
    }

    public void StartShooting()
    {
        if (_shooter == null)
            _shooter = StartCoroutine(Shooter());
    }

    public void StopShooting()
    {
        if (_shooter == null)
            return;

        StopCoroutine(_shooter);
        _shooter = null;
    }
}
