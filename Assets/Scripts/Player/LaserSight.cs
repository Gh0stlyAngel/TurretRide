using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserSight : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform _shootingPoint;
    [SerializeField] private float _maxDistance;

    private void Awake()
    {
        _lineRenderer.positionCount = 2;
        _lineRenderer.useWorldSpace = true;
    }

    private void LateUpdate()
    {
        Vector3 origin = _shootingPoint.position;
        Vector3 direction = _shootingPoint.forward;

        Vector3 endPoint = origin + direction * _maxDistance;

        _lineRenderer.SetPosition(0, origin);
        _lineRenderer.SetPosition(1, endPoint);
    }
}
 