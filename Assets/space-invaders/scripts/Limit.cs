using UnityEngine;

public class Limit : MonoBehaviour
{
    [SerializeField] private GameObject gameManager;
    [SerializeField] private float limitDirection;

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Enemy")) {
            gameManager.GetComponent<SpaceInvadersManager>().hitLimit = true;
        }
        else if (collision.CompareTag("Player")) {
            if (gameManager.GetComponent<SpaceInvadersManager>().blockMovement == false) {
                gameManager.GetComponent<SpaceInvadersManager>().blockMovement = true;
                gameManager.GetComponent<SpaceInvadersManager>().limitDirection = limitDirection;
            } 
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            if (gameManager.GetComponent<SpaceInvadersManager>().blockMovement == true) gameManager.GetComponent<SpaceInvadersManager>().blockMovement = false;
        }
    }
}
