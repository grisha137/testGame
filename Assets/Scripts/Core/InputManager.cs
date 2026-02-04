using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public Vector2 MoveInput { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool JumpHeld { get; private set; }
    public bool DashPressed { get; private set; }
    public bool AttackPressed { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Update()
    {
        var keyboard = Keyboard.current;
        var gamepad = Gamepad.current;

        Vector2 input = Vector2.zero;
        if (keyboard != null)
        {
            input.x += (keyboard.aKey.isPressed ? -1f : 0f) + (keyboard.dKey.isPressed ? 1f : 0f);
            input.y += (keyboard.sKey.isPressed ? -1f : 0f) + (keyboard.wKey.isPressed ? 1f : 0f);
        }

        if (gamepad != null)
        {
            input += gamepad.leftStick.ReadValue();
        }

        MoveInput = Vector2.ClampMagnitude(input, 1f);

        JumpPressed = (keyboard?.spaceKey.wasPressedThisFrame ?? false) || (gamepad?.buttonSouth.wasPressedThisFrame ?? false);
        JumpHeld = (keyboard?.spaceKey.isPressed ?? false) || (gamepad?.buttonSouth.isPressed ?? false);
        DashPressed = (keyboard?.leftShiftKey.wasPressedThisFrame ?? false) || (gamepad?.buttonEast.wasPressedThisFrame ?? false);
        var mouse = Mouse.current;
        AttackPressed = (mouse?.leftButton.wasPressedThisFrame ?? false) || (gamepad?.rightTrigger.wasPressedThisFrame ?? false);
    }
}
