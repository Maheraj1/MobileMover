using UnityEngine;
using UnityEngine.SceneManagement; // Allows Unity to switch game scenes

public class MainMenuManager : MonoBehaviour
{
    // Call this to load into the actual gameplay
    public void StartGame()
    {
        // "GameScene" must match the exact name of your playable level scene
        SceneManager.LoadScene("Desk");
    }

    // Call this to close the game
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit button was pressed!"); // Proves it works inside the editor
    }
}