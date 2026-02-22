using UnityEngine;

public class CollisionManager : MonoBehaviour {
    [SerializeField] GameObject gameManager;

    public void checkCollisions() {
        if(gameManager.GetComponent<SpaceInvadersManager>().hitGameOver) {
            gameManager.GetComponent<SpaceInvadersManager>().hitGameOver = false;
            gameManager.GetComponent<SpaceInvadersManager>().callGameOver();
        }
        else if(gameManager.GetComponent<SpaceInvadersManager>().hitLimit) {
            gameManager.GetComponent<SpaceInvadersManager>().hitLimit = false;
            if (gameManager.GetComponent<SpaceInvadersManager>().firstLimitReached == false) gameManager.GetComponent<SpaceInvadersManager>().limitReached = true;
        }
    }
}
