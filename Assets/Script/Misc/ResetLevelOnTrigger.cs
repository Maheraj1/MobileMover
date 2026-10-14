using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ResetLevelOnTrigger : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            GameManager.Instance.Reset();
        }
    }
}
