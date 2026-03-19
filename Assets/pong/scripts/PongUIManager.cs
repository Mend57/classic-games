using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PongUIManager : MonoBehaviour
{
    [SerializeField] private Text highScore, rightScore, leftScore;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject backgroundMusic;
    [SerializeField] private PongManager gameManager;

    private void Awake() {
        unpause();
        setScore();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) togglePause();
    }

    private void togglePause() {
        if (Time.timeScale == 0f) unpause();
        else pause();
    }

    public void unpause() {
        Cursor.visible = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        backgroundMusic.GetComponent<AudioSource>().volume = backgroundMusic.GetComponent<AudioSource>().volume * 2;
    }

    private void pause() {
        Cursor.visible = true;
        highScore.text = "High Score: " + PongManager.highScore.ToString();
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        backgroundMusic.GetComponent<AudioSource>().volume = backgroundMusic.GetComponent<AudioSource>().volume / 2;
    }

    public void goToMenu() {
        SceneManager.LoadScene(Scenes.MENU);
    }

    public void setScore() {
        rightScore.text = gameManager.rightScore.ToString();
        leftScore.text = gameManager.leftScore.ToString();
    }
}
