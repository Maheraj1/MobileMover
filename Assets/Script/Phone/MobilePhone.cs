using UnityEngine;
using System;

public class MobilePhone : MonoBehaviour {
    public static MobilePhone Instance;
    
    [SerializeField] private AudioClip[] sounds;
    private int _currAudioIndex = 0;
    void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void PlaySound() {
        AudioManager.instance.PlayAudio(sounds[_currAudioIndex]);
        _currAudioIndex++;
        if (_currAudioIndex >= sounds.Length) {
            _currAudioIndex = 0;
        }
    }

    public void Flash() {
        InputManager.instance.TriggerFlash();
    }
}
