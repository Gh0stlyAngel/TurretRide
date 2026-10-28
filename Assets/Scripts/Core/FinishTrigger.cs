using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _gameManager.WinGame();
        }
    }
}
