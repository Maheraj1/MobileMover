using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {
    [SerializeField] AudioClip audioClip;
    private void Start() {
        AudioManager.instance.PlayAudio(audioClip);
    }

    public void ToMainMenu() {
        SceneManager.LoadScene(0);
        AudioManager.instance.Clear();
    }

    public void Reset() {
        LevelManager.Instance.ChangeLevel(-1);
        AudioManager.instance.Clear();
    }
}
