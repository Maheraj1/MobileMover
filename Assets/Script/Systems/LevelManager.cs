using Unity;
using UnityEngine;
using UnityEngine.SceneManagement;

// Manages and stores the level data
public class LevelManager : MonoBehaviour {
    public static LevelManager Instance;

    [SerializeField] private string[] levels;

    void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    // changes level to specified 
    public void ChangeLevel(int index) {
        SceneManager.LoadScene(levels[index], LoadSceneMode.Single);    
    }
}