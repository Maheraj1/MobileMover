using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver1 : MonoBehaviour {
    private void Start() {
    }

    public void ToMainMenu() {
        SceneManager.LoadScene(0);
    }

    public void Reset() {
        LevelManager.Instance.ChangeLevel(0);
    }
}
