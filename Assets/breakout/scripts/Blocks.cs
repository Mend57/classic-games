using UnityEngine;

public class Blocks : MonoBehaviour
{
    [SerializeField] private int points; 
    private BreakoutBall ball;
    private BreakoutManager gameManager;
    [SerializeField] private float newSpeed;

    public void Start() {
        ball = GameObject.FindGameObjectWithTag("Projectile").GetComponent<BreakoutBall>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<BreakoutManager>();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        ball.hasCollidedThisFrame = true;
        ContactPoint2D contact = collision.GetContact(0);
        float absX = Mathf.Abs(contact.normal.x);
        float absY = Mathf.Abs(contact.normal.y);

        if (Mathf.Approximately(absX, absY)) ball.direction *= -1;
        else if (absX > absY) ball.direction.x *= -1;
        else ball.direction.y *= -1;

        if(newSpeed > ball.speed) {
            ball.speed = newSpeed;
        }

        gameManager.GetComponent<BreakoutManager>().scored(points);
        Destroy(gameObject);
        if(gameManager.getBlocksAmount() <= 0) {
            gameManager.reinstantiateBlocks();
            ball.callResetBall();
        }
    }
}
