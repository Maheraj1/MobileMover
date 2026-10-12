using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable {
    public static Player Instance;
    public event EventHandler<IDamageable.OnDamageTakenEventArgs> OnDamageTaken;
    public event EventHandler<EventArgs> OnDie;
    public event EventHandler<OnChargeChangedEventArgs> OnChargeChanged;

    public class OnChargeChangedEventArgs : EventArgs {
        public float NewCharge;
    }
    
    [Header("Movement Settings")]
    [Tooltip("Move speed of the player in m/s")]
    [SerializeField] private float moveSpeed = 1;
    [Tooltip("Move speed of the player in rad/s")]
    [SerializeField] private float rotateSpeed = 1;
    
    [Header("Charge Settings")]
    [Tooltip("The amount of maximum charge the phone can hold")]
    [SerializeField] private float maxCapacity = 100f;
    [Tooltip("Rate at which charge loss occur in % charge per N")]
    [SerializeField] private float chargeLossRate = .1f;
    
    private Rigidbody2D _rb;
    [SerializeField] private int health = 200;
    private IDamageable _damageableImplementation;
    private float _charge = 100f;

    void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    
    void Start() {
        _rb = GetComponent<Rigidbody2D>();
        // InputManager.instance.OnMovePressed += InstanceOnOnMovePressed_Handler;
    }

    private void InstanceOnOnMovePressed_Handler(object sender, InputManager.OnMovePressedEventArgs e) {
        AddMovementForce(e.Move);
    }

    private void Update() {
        AddMovementForce(InputManager.instance.ReadMove(), true);
    }

    private void AddMovementForce(Vector2 move, bool useDeltaTime = false) {
        if (_charge <= 0f) return;
        Vector2 force = transform.right * (move.y * moveSpeed * (useDeltaTime ? Time.deltaTime : 1));
        _rb.AddForce(force, ForceMode2D.Impulse);
        _rb.AddTorque(move.x * -rotateSpeed * (useDeltaTime ? Time.deltaTime : 1), 
                        ForceMode2D.Impulse
        );
        
        _charge = Mathf.Clamp(_charge - (chargeLossRate * force.magnitude), 0f, maxCapacity);
        OnChargeChanged?.Invoke(this, new OnChargeChangedEventArgs {
            NewCharge = _charge
        });
    }

    public void TakeDamage(int amount) {
        health -= amount;
        
        // Autocorrect -ve health
        if (health <= 0) {
            health = 0;
            OnDie?.Invoke(this, EventArgs.Empty);
        }
        
        OnDamageTaken?.Invoke(this, new IDamageable.OnDamageTakenEventArgs {
            Damage = amount,
            Health = health
        });
    }

    public void Charge(float amount) {
        _charge = Mathf.Clamp(_charge + amount, 0f, maxCapacity);
        OnChargeChanged?.Invoke(this, new OnChargeChangedEventArgs {
            NewCharge = _charge
        });
    }

    public int GetHealth() {
        return health;
    }

    public float MaxCharge() {
        return maxCapacity;
    }
}
