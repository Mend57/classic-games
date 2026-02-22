using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpaceInvadersUIManager : MonoBehaviour {
    [SerializeField] private Text highScore, score, finalScore, finalHighScore;
    [SerializeField] private GameObject pauseMenu, gameOverMenu;
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject backgroundMusic;
    [SerializeField] private GameObject[] lives;

    public void setLife() {
        for (int i = 0; i < lives.Length; i++) {
            lives[i].SetActive(i < gameManager.GetComponent<SpaceInvadersManager>().life);
        }
    }
    
    private void Awake() {
        unpause();
        score.text = gameManager.GetComponent<SpaceInvadersManager>().score.ToString();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) togglePause();
    }

    private void togglePause() {
        if (Time.timeScale == 0f) unpause();
        else pause();
    }

    public void unpause() {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        backgroundMusic.GetComponent<AudioSource>().volume = backgroundMusic.GetComponent<AudioSource>().volume * 2;
    }

    private void pause() {
        highScore.text = "High Score: " + SpaceInvadersManager.highScore.ToString();
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        backgroundMusic.GetComponent<AudioSource>().volume = backgroundMusic.GetComponent<AudioSource>().volume / 2;
    }

    public void goToMenu() {
        SceneManager.LoadScene(Scenes.MENU);
    }

    public void restartGame() {
        SceneManager.LoadScene(Scenes.SPACE_INVADERS);
    }

    public void setScore() {
        score.text = gameManager.GetComponent<SpaceInvadersManager>().score.ToString();
    }

    public void gameOver() {
        finalHighScore.text = "High Score: " + SpaceInvadersManager.highScore.ToString();
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
        backgroundMusic.GetComponent<AudioSource>().volume = backgroundMusic.GetComponent<AudioSource>().volume / 6;
        finalScore.text = gameManager.GetComponent<SpaceInvadersManager>().score.ToString();
    }
}
