using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;

    [SerializeField] private Transform warningPosition; 
    [SerializeField] private Transform lowPowerPrefab;
    [SerializeField] private float warningTime = 2.0f;

    private float volume = 50f;
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
        if (e.state == Player.State.LowPower && e.prevState == Player.State.Normal) {
            MessageDisplayManager.Instance.DisplayMessage("<color=#FFCC00>Low Power. Find charger!</color>");
        }
    }
    public Player GetPlayer() {
        return Player.Instance;
    }

    public void Reset() {
        LevelManager.Instance.ChangeLevel(-1);
    }
    
    public float GetVolume() {
        return volume;
    }

    public void SetVolume(float value) {
        volume = value;
    }
}
