using UnityEngine;

public class SaveManager : MonoBehaviour {
    public static SaveManager Instance;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
            return;
        }
    }

    void Start() {
        loadGame();
    }

    private void loadGame() {
        Frog.highScore = PlayerPrefs.GetInt("FroggerHS");
        SpaceInvadersManager.highScore = PlayerPrefs.GetInt("SpaceInvadersHS");
        PongManager.highScore = PlayerPrefs.GetInt("PongHS");
        BreakoutManager.highScore = PlayerPrefs.GetInt("BreakoutHS");
    }

    public void saveGame() {
        PlayerPrefs.SetInt("FroggerHS", Frog.highScore);
        PlayerPrefs.SetInt("SpaceInvadersHS", SpaceInvadersManager.highScore);
        PlayerPrefs.SetInt("PongHS", PongManager.highScore);
        PlayerPrefs.SetInt("BreakoutHS", BreakoutManager.highScore);
    }
}
