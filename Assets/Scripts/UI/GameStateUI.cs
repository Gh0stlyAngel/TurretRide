using TMPro;
using UnityEngine;

public class GameStateUI : MonoBehaviour
{
    [SerializeField] private GameObject _statePanel;
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private TMP_Text _hintText;
    [SerializeField] private GameManager _gameManager;

    [SerializeField] private Color _waitingColor = Color.white;
    [SerializeField] private Color _winColor = Color.green;
    [SerializeField] private Color _loseColor = Color.red;

    private void OnEnable()
    {
        _gameManager.GameStateChanged += OnGameStateChanged;
        OnGameStateChanged(_gameManager.CurrentState);
    }

    private void OnDisable()
    {
        _gameManager.GameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.Waiting:
                _titleText.color = _waitingColor;
                _titleText.text = "READY?";
                _hintText.text = "Tap to Start";
                _statePanel.SetActive(true);
                break;

            case GameState.Playing:
                _statePanel.SetActive(false);
                break;

            case GameState.Lose:
                _titleText.color = _loseColor;
                _titleText.text = "YOU LOSE!";
                _hintText.text = "Tap to Restart";
                _statePanel.SetActive(true);
                break;

            case GameState.Win:
                _titleText.color = _winColor;
                _titleText.text = "YOU WIN!";
                _hintText.text = "Tap to Restart";
                _statePanel.SetActive(true);
                break;
        }
    }
}
