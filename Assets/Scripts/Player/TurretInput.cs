using UnityEngine;
using UnityEngine.InputSystem;

public class TurretInput : MonoBehaviour
{
    [SerializeField] private TurretRotation _turretRotation;
    [SerializeField] private GameManager _gameManager;
    private void Update()
    {
        if (_gameManager.CurrentState != GameState.Playing)
            return;

        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            _turretRotation.Rotate(Touchscreen.current.primaryTouch.delta.ReadValue().x);
        }
        else if (Mouse.current != null && Mouse.current.leftButton.isPressed)
        {
            _turretRotation.Rotate(Mouse.current.delta.ReadValue().x);
        }
    }
}
