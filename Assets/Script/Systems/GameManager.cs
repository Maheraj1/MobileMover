using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;

    [SerializeField] private Transform warningPosition; 
    [SerializeField] private Transform lowPowerPrefab;
    [SerializeField] private float warningTime = 2.0f;
    
    void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    
    void Start() {
        Player.Instance.OnStateChanged += OnStateChanged_Handler;
    }

    private void OnStateChanged_Handler(object sender, Player.OnStateChangedEventArgs e) {
        if (e.state == Player.State.LowPower) {
            MessageDisplayManager.Instance.DisplayMessage("<color=#FFCC00>Low Power. Find charger!</color>");
        }
    }

    void Update() {
        
    }

    public Player GetPlayer() {
        return Player.Instance;
    }

    public void Reset() {
        LevelManager.Instance.ChangeLevel(-1);
    }
}
