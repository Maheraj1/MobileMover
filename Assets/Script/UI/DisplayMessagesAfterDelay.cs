using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayMessagesAfterDelay : MonoBehaviour {
    [Serializable] 
    public struct Message {
        public string message;
        public float delay;
    }
    [SerializeField] private List<Message> messages = new List<Message>();
    
    void Start() {
        if (messages.Count == 0) return;
        StartCoroutine(DisplayMessage(0));
    }

    IEnumerator DisplayMessage(int index) {
        yield return new WaitForSeconds(messages[index].delay);
        MessageDisplayManager.Instance.DisplayMessage(messages[index].message);
        if (messages.Count - 1 != index) {
            StartCoroutine(DisplayMessage(index + 1));
        }
    }
}
