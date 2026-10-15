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
    }

    public void Reset() {
        LevelManager.Instance.ChangeLevel(-1);
    }
}
