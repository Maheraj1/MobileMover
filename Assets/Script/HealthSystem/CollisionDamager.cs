using System;
using UnityEngine;

public class CollisionDamager : MonoBehaviour {
    [SerializeField] private int damageAmount;
    private void OnCollisionEnter2D(Collision2D other) {
        other.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable);
        if (damageable != null) {
            damageable.TakeDamage(damageAmount);
        }
    }
}
