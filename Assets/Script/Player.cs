using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable {
    public static Player Instance;
    public event EventHandler<IDamageable.OnDamageTakenEventArgs> OnDamageTaken;
    public event EventHandler<EventArgs> OnDie;
    public event EventHandler<OnChargeChangedEventArgs> OnChargeChanged;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;

    public class OnStateChangedEventArgs : EventArgs {
        public State state;
    }

    public class OnChargeChangedEventArgs : EventArgs {
        public float NewCharge;
    }

    [SerializeField] private GameObject flashLight;
    
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
    [Tooltip("Rate at which charge loss occur in % charge per N m")]
    [SerializeField] private float chargeLossRateRotation = .1f;
    [Tooltip("How much fraction of loss will be reduced in movement")]
    [SerializeField] private float lowPowerLossDebuff = .1f;
    
    private Rigidbody2D _rb;
    [SerializeField] private int health = 200;
    private IDamageable _damageableImplementation;
    private float _charge = 100f;

    public enum State {
        FullyCharged,
        Normal,     // 100 - 80 % battery
        LowPower,   // 20 - 10  % battery
        VeryLowPower// 10       % battery
    }

    private State _state = State.Normal;

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
        InputManager.instance.OnFlashPressed += (sender, args) => {
            if (flashLight)
                flashLight.SetActive(!flashLight.activeSelf);
        };
    }

    private void InstanceOnOnMovePressed_Handler(object sender, InputManager.OnMovePressedEventArgs e) {
        AddMovementForce(e.Move);
    }

    private void Update() {
        switch (_charge) {
            case > 20: {
                if (_state != State.Normal) OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                    state = State.Normal
                });
                _state = State.Normal;
                break;
            }
            case <= 10: {
                if (_state != State.VeryLowPower) OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                    state = State.VeryLowPower
                });
                _state = State.VeryLowPower;
                break;
            }
            default: {
                if (_state != State.LowPower) OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                    state = State.LowPower
                });
                _state = State.LowPower;
                break;
            }
        }

        AddMovementForce(InputManager.instance.ReadMove(), true);
    }

    private void AddMovementForce(Vector2 move, bool useDeltaTime = false) {
        if (_charge <= 0f) return;

        float factor = (_state == State.LowPower || _state == State.VeryLowPower) ? 1.0f - lowPowerLossDebuff : 1.0f;
        
        Vector2 force = transform.right *  (factor * move.y * moveSpeed * (useDeltaTime ? Time.deltaTime : 1));
        float torque = move.x * -rotateSpeed * factor * (useDeltaTime ? Time.deltaTime : 1);
        _rb.AddForce(force, ForceMode2D.Impulse);
        _rb.AddTorque(torque, ForceMode2D.Impulse);
        
        _charge = Mathf.Clamp(
            _charge - (chargeLossRate * force.magnitude)
                    - (chargeLossRateRotation * Mathf.Abs(torque)),
            0f, maxCapacity);
        
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
        if (!Mathf.Approximately(_charge, maxCapacity))
            return;
        if (_state != State.FullyCharged) 
            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                state = State.FullyCharged
            });
        _state = State.FullyCharged;
    }

    public int GetHealth() {
        return health;
    }

    public float MaxCharge() {
        return maxCapacity;
    }
}
