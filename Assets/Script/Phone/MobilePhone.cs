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
        var go = new GameObject();
        var audio_s = go.AddComponent<AudioSource>();
        audio_s.volume = GameManager.Instance.GetVolume();
        audio_s.clip = sounds[_currAudioIndex];
        Destroy(go, sounds[_currAudioIndex].length);

        _currAudioIndex++;
        if (_currAudioIndex >= sounds.Length) {
            _currAudioIndex = 0;
        }
    }

    public void Flash() {
        InputManager.instance.TriggerFlash();
    }
}
