using UnityEngine;

public class Limits : MonoBehaviour
{
    [SerializeField] private bool leftLimit;
    [SerializeField] private PongManager gameManager;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (leftLimit) gameManager.pointRight();
        else gameManager.pointLeft();
        collision.GetComponent<Ball>().resetBall();
    }
}
