using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;

    [SerializeField] private Transform warningPosition; 
    [SerializeField] private Transform lowPowerPrefab;
    [SerializeField] private float warningTime = 2.0f;
    
    private Player _player;
    void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        _player.OnStateChanged += OnStateChanged_Handler;
    }

    private void OnStateChanged_Handler(object sender, Player.OnStateChangedEventArgs e) {
        if (e.state == Player.State.LowPower) {
            DisplayLowPower();
        }
    }

    void Update() {
        
    }

    public Player GetPlayer() {
        return _player;
    }

    public void RegisterPlayer(Player player) {
        _player = player;
    }
    
    private void DisplayLowPower() {
        Transform go = Instantiate(lowPowerPrefab, warningPosition);
        go.GetComponent<TextMeshProUGUI>().text = "Low Power. Find charger!";
        Destroy(go.gameObject, warningTime);
    }
}
