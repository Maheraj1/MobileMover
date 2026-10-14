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
            DisplayLowPower();
        }
    }

    void Update() {
        
    }

    public Player GetPlayer() {
        return Player.Instance;
    }
    
    private void DisplayLowPower() {
        Transform go = Instantiate(lowPowerPrefab, warningPosition);
        go.GetComponent<TextMeshProUGUI>().text = "Low Power. Find charger!";
        Destroy(go.gameObject, warningTime);
    }
}
