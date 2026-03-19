using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BreakoutUIManager : MonoBehaviour
{
    [SerializeField] private Text highScore, score;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject backgroundMusic;
    [SerializeField] private BreakoutManager gameManager;

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
        highScore.text = "High Score: " + BreakoutManager.highScore.ToString();
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        backgroundMusic.GetComponent<AudioSource>().volume = backgroundMusic.GetComponent<AudioSource>().volume / 2;
    }

    public void goToMenu() {
        SceneManager.LoadScene(Scenes.MENU);
    }

    public void setScore() {
        score.text = gameManager.score.ToString();
    }

    public void restart() {
        setScore();
        backgroundMusic.GetComponent<AudioSource>().Stop();
        backgroundMusic.GetComponent<AudioSource>().Play();
    }
}
