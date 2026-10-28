using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _detectionRange;
    private Transform _target;
    private bool _isChasing;
    private bool _canMove;

    private GameManager _gameManager;

    public bool IsMoving => _isChasing && _canMove;
    public event Action<bool> MovementStateChanged;

    public void Initialize(Transform target, GameManager gameManager)
    {
        _target = target;
        _gameManager = gameManager;
        _gameManager.GameStateChanged += OnGameStateChanged;
        OnGameStateChanged(_gameManager.CurrentState);
    }

    private void OnDestroy()
    {
        if (_gameManager != null)
            _gameManager.GameStateChanged -= OnGameStateChanged;
    }

    private void Update()
    {
        if (!_canMove ||_target == null)
            return;

        Vector3 targetPosition = _target.position;
        targetPosition.y = transform.position.y;

        Vector3 currentDirection = targetPosition - transform.position;
        float currentDistance = currentDirection.magnitude;

        if (!_isChasing && currentDistance <= _detectionRange)
        {
            _isChasing = true;
            MovementStateChanged?.Invoke(IsMoving);
        }

        if (_isChasing)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);

            if (currentDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(currentDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            }
                
        }
    }

    private void OnGameStateChanged(GameState state)
    {
        _canMove = state == GameState.Playing;
        MovementStateChanged?.Invoke(IsMoving);
    }
}
