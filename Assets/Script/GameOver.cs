using System;
using UnityEngine;

public class GameOver : MonoBehaviour {
    [SerializeField] AudioClip audioClip;
    private void Start() {
        AudioManager.instance.PlayAudio(audioClip);
    }

    public void ToMainMenu() {
        LevelManager.Instance.ChangeLevel(0);
    }

    public void Reset() {
        LevelManager.Instance.ChangeLevel(-1);
    }
}
