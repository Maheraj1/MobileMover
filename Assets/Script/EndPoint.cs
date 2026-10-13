using System;
using UnityEngine;

public class EndPoint : MonoBehaviour {
    [SerializeField] private Transform light;

    private void Update() {
        Vector3 dir = transform.position - GameManager.Instance.GetPlayer().transform.position;
        
    }
}