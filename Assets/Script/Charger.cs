using System;
using UnityEngine;

public class Charger : MonoBehaviour {
    [Tooltip("How fast does the charger charge in %/s")]
    [SerializeField] private float chargingSpeed;
    [Tooltip("Index of the level that will be loaded next in Level Manager (-1 for don't change)")]
    [SerializeField] private int nextLevel = -1;
    bool _isCharging = false;

    private void Start() {
        Player.Instance.OnStateChanged += (sender, args) => {
            if (args.state == Player.State.FullyCharged && nextLevel != -1)
                LevelManager.Instance.ChangeLevel(nextLevel);
        };
    }

    void Update() {
        if (_isCharging)
            Player.Instance.Charge(chargingSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            _isCharging = true;
        }
    }
    
    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            _isCharging = false;
        }
    }
}
