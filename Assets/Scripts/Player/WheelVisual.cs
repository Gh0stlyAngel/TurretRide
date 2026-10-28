using UnityEngine;

public class WheelVisual : MonoBehaviour
{
    [SerializeField] private CarMovement _carMovement;
    [SerializeField] private Transform[] _wheels;

    private float _carSpeed;
    private float[] _wheelRadius;

    private void Start()
    {
        _carSpeed = _carMovement.Speed;
        _wheelRadius = new float[_wheels.Length];

        for (int i = 0; i < _wheels.Length; i++)
        {
            Transform wheel = _wheels[i];
            MeshFilter meshFilter = wheel.GetComponent<MeshFilter>();

            Vector3 meshSize = meshFilter.sharedMesh.bounds.size;
            Vector3 scale = wheel.lossyScale;

            float diameterY = meshSize.y * Mathf.Abs(scale.y);
            float diameterZ = meshSize.z * Mathf.Abs(scale.z);

            _wheelRadius[i] = (diameterY + diameterZ) * 0.25f;
        }
    }

    private void Update()
    {
        if (!_carMovement.IsMoving)
            return;

        for (int i = 0; i < _wheels.Length; i++)
        {
            float angle = _carSpeed / _wheelRadius[i] * Mathf.Rad2Deg * Time.deltaTime;

            _wheels[i].Rotate(Vector3.right, angle, Space.Self);
        }
    }
}