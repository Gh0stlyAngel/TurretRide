using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    private bool _isMoving;
    public float Speed => _speed;
    public bool IsMoving => _isMoving;
    private void Update()
    {
        if (_isMoving)
            transform.position += _speed * Time.deltaTime * transform.forward;
    }

    public void StartMoving()
    {
        _isMoving = true;
    }

    public void StopMoving()
    {
        _isMoving = false;
    }

}
