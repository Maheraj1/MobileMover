using System;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable {
    public event EventHandler<IDamageable.OnDamageTakenEventArgs> OnDamageTaken;
    public event EventHandler<EventArgs> OnDie;

    [Tooltip("Move speed of the player in m/s")]
    [SerializeField] private float moveSpeed = 1;
    [Tooltip("Move speed of the player in rad/s")]
    [SerializeField] private float rotateSpeed = 1;
    [SerializeField] private Vector3 forward = Vector3.forward;
    private Rigidbody2D _rb;
    [SerializeField] private int health = 200;
    private IDamageable _damageableImplementation;

    void Start() {
        _rb = GetComponent<Rigidbody2D>();
        // InputManager.instance.OnMovePressed += InstanceOnOnMovePressed_Handler;
    }

    private void InstanceOnOnMovePressed_Handler(object sender, InputManager.OnMovePressedEventArgs e) {
        _rb.AddForce(transform.right * (e.Move.y * moveSpeed), ForceMode2D.Impulse);
        _rb.AddTorque(e.Move.x * -rotateSpeed, ForceMode2D.Impulse);
    }

    private void Update() {
        Vector2 move = InputManager.instance.ReadMove();
        _rb.AddForce(transform.right * (move.y * moveSpeed), ForceMode2D.Impulse);
        _rb.AddTorque(move.x * -rotateSpeed, ForceMode2D.Impulse);
    }

    public void TakeDamage(int amount) {
        health -= amount;
        
        // Autocorrect -ve health
        if (health < 0)
            health = 0;
        
        OnDamageTaken?.Invoke(this, new IDamageable.OnDamageTakenEventArgs {
            Damage = amount,
            Health = health
        });
    }

    public int GetHealth() {
        return health;
    }
}
