using UnityEngine;

public class PongManager : MonoBehaviour
{
    [SerializeField] private PongUIManager UIManager;
    [SerializeField] private GameObject scoreSound, highScoreSound, enemyScoreSound;
    public int leftScore = 0, rightScore = 0;
    public static int highScore = 0;
    private bool hasPlayedHighScoreSound = false;

    public void pointLeft() {
        leftScore++;
        UIManager.setScore();
        enemyScoreSound.GetComponent<AudioSource>().Play();
    }

    public void pointRight() {
        rightScore++;
        UIManager.setScore();
        if (rightScore > highScore) {
            highScore = rightScore;
            if (!hasPlayedHighScoreSound) {
                highScoreSound.GetComponent<AudioSource>().Play();
                hasPlayedHighScoreSound = true;
                return;
            }
        }
        scoreSound.GetComponent<AudioSource>().Play();
    }
}
