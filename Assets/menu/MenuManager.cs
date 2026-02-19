using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
    [SerializeField] private Button exitButton, froggerButton, spaceInvadersButton, breakout, pong;

    public void exit() {
        Application.Quit();
    }

    public void frogger() {
        SceneManager.LoadScene(Scenes.FROGGER);
    }

    public void spaceInvaders() {
        SceneManager.LoadScene(Scenes.SPACE_INVADERS);
    }
}
