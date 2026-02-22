using UnityEngine;

public class GameOverLimit : MonoBehaviour
{

    [SerializeField] GameObject gameManager;
    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Enemy")) {
            gameManager.GetComponent<SpaceInvadersManager>().hitGameOver = true;
        }
    }
}
