using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {
    public void StartGame() {
        LevelManager.Instance.ChangeLevel(0);
    }

    // Call this to close the game
    public void QuitGame() {
        Application.Quit();
    }
}