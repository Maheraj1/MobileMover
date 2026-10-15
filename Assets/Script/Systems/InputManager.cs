using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour {
    public static InputManager instance;
    public event EventHandler<OnMovePressedEventArgs> OnMovePressed;
    public event EventHandler<EventArgs> OnFlashPressed;
    private InputSystem_Actions _actions;
    private bool paused = false;

    public class OnMovePressedEventArgs : EventArgs {
        public Vector2 Move;
    }

    void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        }

        instance = this;
        _actions = new InputSystem_Actions();
        _actions.Player.Enable();
        _actions.Player.Move.started += MoveOnstarted_Handler;
        _actions.Player.Flash.started += OnFlash_Handler;
        _actions.Player.Pause.started += Pause_Handler;
    }

    private void Pause_Handler(InputAction.CallbackContext obj) {
        paused = !paused;
        MessageDisplayManager.Instance.DisplayMessage(
            paused ? "<color=green>Paused</color>" : 
                    "<color=green>Unpaused</color>");
    }

    private void OnFlash_Handler(InputAction.CallbackContext obj) {
        if (paused) return;
        OnFlashPressed?.Invoke(this, EventArgs.Empty);
    }

    private void MoveOnstarted_Handler(InputAction.CallbackContext obj) {
        if (paused) return;
        OnMovePressed?.Invoke(this, new OnMovePressedEventArgs { Move = obj.ReadValue<Vector2>() });
    }

    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void TriggerFlash() {
        if (paused) return;
        OnFlashPressed?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 ReadMove() {
        if (paused) return new Vector2(0f, 0f);
        return _actions.Player.Move.ReadValue<Vector2>();
    } 
}
