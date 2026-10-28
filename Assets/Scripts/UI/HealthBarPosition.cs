using UnityEngine;

[DefaultExecutionOrder(100)]
public class HealthBarPosition : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _target;

    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        Vector3 screenPosition =
            _camera.WorldToScreenPoint(_target.position);

        _rectTransform.position = screenPosition;
    }
}