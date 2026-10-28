using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameState _gameState = GameState.Waiting;
    public GameState CurrentState => _gameState;
    [SerializeField] private CarMovement _carMovement;
    [SerializeField] private TurretShooting _turretShooting;
    [SerializeField] private Health _carHealth;

    public event Action<GameState> GameStateChanged;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        bool isPressed = (Touchscreen.current != null && 
            Touchscreen.current.primaryTouch.press.wasPressedThisFrame) || 
            (Mouse.current != null && 
            Mouse.current.leftButton.wasPressedThisFrame);

        if (!isPressed)
            return;

        if (_gameState == GameState.Waiting)
        {
            StartGame();
        }
        else if(_gameState == GameState.Win || _gameState == GameState.Lose)
        {
            RestartGame();
        }
    }

    private void OnEnable()
    {
        _carHealth.Died += LoseGame;
    }

    private void OnDisable()
    {
        _carHealth.Died -= LoseGame;
    }

    private void SetGameState(GameState state)
    {
        if (_gameState == state)
            return;

        _gameState = state;
        GameStateChanged?.Invoke(state);
    }

    public void StartGame()
    {
        if (_gameState == GameState.Waiting)
        {
            SetGameState(GameState.Playing);
            _carMovement.StartMoving();
            _turretShooting.StartShooting();
        }
    }

    public void LoseGame()
    {
        if ( _gameState == GameState.Playing)
        {
            SetGameState(GameState.Lose);
            _carMovement.StopMoving();
            _turretShooting.StopShooting();
        }
    }

    public void WinGame()
    {
        if (_gameState == GameState.Playing)
        {
            SetGameState(GameState.Win);
            _carMovement.StopMoving();
            _turretShooting.StopShooting();
        }
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
