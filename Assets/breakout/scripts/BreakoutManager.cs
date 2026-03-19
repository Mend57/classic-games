using UnityEngine;
using UnityEngine.SceneManagement;

public class BreakoutManager : MonoBehaviour {
    [SerializeField] private BreakoutUIManager UIManager;
    [SerializeField] private GameObject scoreSound, highScoreSound, deathSound, rows;
    [SerializeField] private GameObject[] LifeBar;
    private GameObject instantiatedRows;
    public int score = 0;
    public static int highScore = 0;
    private bool hasPlayedHighScoreSound = false;
    private int hitpoints, blocksAmount;
    private const int INITIAL_BLOCKS_AMOUNT = 112, INITIAL_HITPOINTS = 3;

    public void Awake() {
        instantiatedRows = Instantiate(rows);
        hitpoints = INITIAL_HITPOINTS;
        blocksAmount = INITIAL_BLOCKS_AMOUNT;
    }

    public void scored(int points) {
        score += points;
        blocksAmount--;
        UIManager.setScore();
        if (score > highScore) {
            highScore = score;
            if (!hasPlayedHighScoreSound) {
                highScoreSound.GetComponent<AudioSource>().Play();
                hasPlayedHighScoreSound = true;
                return;
            }
        }
        scoreSound.GetComponent<AudioSource>().Play();
    }

    public int getHP() {
        return hitpoints;
    }

    public int getBlocksAmount() {
        return blocksAmount;
    }

    public void decreaseHP() {
        deathSound.GetComponent<AudioSource>().Play();
        LifeBar[--hitpoints].SetActive(false);
    }

    public void resetGame() {
        score = 0;
        hasPlayedHighScoreSound = false;
        reinstantiateBlocks();
    }

    public void reinstantiateBlocks() {
        Destroy(instantiatedRows);
        instantiatedRows = Instantiate(rows);
        blocksAmount = INITIAL_BLOCKS_AMOUNT;
        UIManager.restart();
    }

    public void resetLifeBar() {
        if (hitpoints <= 0) {
            hitpoints = INITIAL_HITPOINTS;
            foreach (GameObject lifeSlot in LifeBar) lifeSlot.SetActive(true);
        }
    }
}

