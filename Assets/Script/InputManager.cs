using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour {
    public static InputManager instance;
    public event EventHandler<OnMovePressedEventArgs> OnMovePressed; 
    private InputSystem_Actions _actions;

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
    }

    private void MoveOnstarted_Handler(InputAction.CallbackContext obj) {
        OnMovePressed?.Invoke(this, new OnMovePressedEventArgs { Move = obj.ReadValue<Vector2>() });
    }

    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public Vector2 ReadMove() {
        return _actions.Player.Move.ReadValue<Vector2>();
    } 
}
