using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _carTransform;
    [SerializeField] private Vector3 _cameraOffset;

    private void LateUpdate()
    {
        transform.position = _carTransform.position + _cameraOffset;
    }
}
