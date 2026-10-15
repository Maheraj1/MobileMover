using Unity;
using UnityEngine;
using UnityEngine.SceneManagement;

// Manages and stores the level data
public class LevelManager : MonoBehaviour {
    public static LevelManager Instance;

    [SerializeField] private string[] levels;
    [SerializeField] private AudioClip resetAudio;
    
    private int _currentLevel = -1;

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
        if (index == -1) {
            SceneManager.LoadScene(levels[_currentLevel], LoadSceneMode.Single);
            AudioManager.instance.PlayAudio(resetAudio);
            return;
        }
        _currentLevel = index;
        SceneManager.LoadScene(levels[index], LoadSceneMode.Single);    
    }
}