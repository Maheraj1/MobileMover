using System;
using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager instance;
    [SerializeField] private AudioClip bgClip;
    private AudioSource _source;
    private int _playingID = 0;
    void Awake() {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        _source = GetComponent<AudioSource>();
    }

    private void Start() {
        _source.clip = bgClip;
        _source.Play();
    }

    public void Clear() {
        _source.clip = bgClip;
        _source.Play();
        _playingID = 0;
    }

    public void PlayAudio(AudioClip audioClip) {
        StartCoroutine(PlayAudio(audioClip, ++_playingID));
    }

    private IEnumerator PlayAudio(AudioClip audioClip, int id) {
        if (_playingID != id) yield break;
        _source.clip = audioClip;
        _source.volume = 1f;
        _source.Play(0);
        yield return new WaitForSeconds(audioClip.length);
        if (_playingID != id) yield break;
        _source.clip = bgClip;
        _source.volume = .3f;
        _source.Play(0);
        _playingID = 0;
    }
}
