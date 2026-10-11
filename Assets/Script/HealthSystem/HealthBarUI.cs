using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour {
    [Tooltip("The object with IDamageable Component.")]
    [SerializeField] private Transform healthBody;
    [Header("References")]
    [Tooltip("The object to follow the global rotation and position.")]
    [SerializeField] private Transform targetBody;
    [SerializeField] private Vector2 offset = Vector2.zero;
    [Tooltip("Lock the rotation of this object (i.e. don't change rotation of this object with change in rotation of target object)")]
    [SerializeField] private bool lockRotation = false;
    [Tooltip("Lock the position of this object (i.e. don't change position of this object with change in position of target object)")]
    [SerializeField] private bool lockPosition = false;
    
    [Header("UI References")]
    [SerializeField] private Slider slider;
    
    private IDamageable _damageable;
    private void Start() {
        healthBody.TryGetComponent(out _damageable);
        if (_damageable == null) {
            Debug.LogError($"{healthBody.name} has no IDamageable Component!");
            Destroy(gameObject);
            return;
        }
        
        _damageable.OnDamageTaken += OnDamageTaken_Handler;
        slider.maxValue = _damageable.GetHealth();
        slider.minValue = 0;
        slider.value = _damageable.GetHealth();
    }

    private void OnDamageTaken_Handler(object sender, IDamageable.OnDamageTakenEventArgs e) {
        slider.value = e.Health;
    }

    private void LateUpdate() {
        
        if (!lockPosition)
            transform.position = targetBody.position + new Vector3(offset.x, offset.y, 0);
        
        if (!lockRotation)
            transform.rotation = targetBody.rotation;
    }

    private void OnDrawGizmos() {
        if (targetBody == null) return;
        
        if (!lockPosition)
            transform.position = targetBody.position + new Vector3(offset.x, offset.y, 0);
        
        if (!lockRotation)
            transform.rotation = targetBody.rotation;
    }
}
