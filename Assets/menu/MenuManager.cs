using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
    [SerializeField] private Button exitButton, froggerButton, spaceInvadersButton, breakoutButton, pongButton;

    void Awake() {
        Cursor.visible = true;
    }

    public void exit() {
        Application.Quit();
    }

    public void frogger() {
        SceneManager.LoadScene(Scenes.FROGGER);
    }

    public void spaceInvaders() {
        SceneManager.LoadScene(Scenes.SPACE_INVADERS);
    }

    public void pong() {
        SceneManager.LoadScene(Scenes.PONG);
    }

    public void breakout() {
        SceneManager.LoadScene(Scenes.BREAKOUT);
    }
}
