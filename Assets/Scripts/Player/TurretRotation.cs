using UnityEngine;

public class TurretRotation : MonoBehaviour
{
    [SerializeField] private Transform _turret;
    [SerializeField] private float _sensitivity = 0.4f;
    [SerializeField] private float _maxAngle = 80f;
    private float _currentAngle;

    public void Rotate(float delta)
    {
        _currentAngle += delta * _sensitivity;
        _currentAngle = Mathf.Clamp(_currentAngle, -_maxAngle, _maxAngle);
        _turret.localRotation = Quaternion.Euler(0f, _currentAngle, 0f);
    }
}
 