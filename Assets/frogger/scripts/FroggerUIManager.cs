using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FroggerUIManager : MonoBehaviour {
    [SerializeField] private Text highScore, score;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject backgroundMusic;

    private void Awake() {
        unpause();
        score.text = player.GetComponent<Frog>().getScore().ToString();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) togglePause();
    }

    private void togglePause() {
        if(Time.timeScale == 0f) unpause();
        else pause();
    }

    public void unpause() {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        backgroundMusic.GetComponent<AudioSource>().volume = backgroundMusic.GetComponent<AudioSource>().volume * 2;
    }

    private void pause() {
        highScore.text = "High Score: " + Frog.getHighScore().ToString();
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        backgroundMusic.GetComponent<AudioSource>().volume = backgroundMusic.GetComponent<AudioSource>().volume / 2;
    }

    public void goToMenu() {
        SceneManager.LoadScene(Scenes.MENU);
    }

    public void setScore() {
        score.text = player.GetComponent<Frog>().getScore().ToString();
    }
}
