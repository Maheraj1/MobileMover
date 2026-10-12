using UnityEngine;

public class Charger : MonoBehaviour {
    [Tooltip("How fast does the charger charge in %/s")]
    [SerializeField] private float chargingSpeed;
    bool _isCharging = false;
    void Update() {
        Player.Instance.Charge(chargingSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            _isCharging = true;
        }
    }
    
    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            _isCharging = true;
        }
    }
}
