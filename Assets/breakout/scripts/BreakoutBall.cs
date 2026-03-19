using System.Collections;
using UnityEngine;

public class BreakoutBall : MonoBehaviour
{
    private static readonly WaitForSeconds waitFor1Second = new(1f);
    public float speed;
    private const float INITIAL_SPEED = 5;
    [SerializeField] private GameObject ballSound;
    [SerializeField] private Paddle paddle;
    [SerializeField] private BreakoutManager gameManager;
    private Rigidbody2D rb;
    public Vector2 direction;
    public bool hasCollidedThisFrame = false;

    void LateUpdate() {
        hasCollidedThisFrame = false;
    }

    void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        speed = INITIAL_SPEED;
        StartCoroutine(resetBall());
    }

    void FixedUpdate() {
        rb.linearVelocity = direction * speed;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        ballSound.GetComponent<AudioSource>().Play();
        if (collision.gameObject.CompareTag("Player")) {
            float paddleWidth = collision.collider.bounds.size.x;
            float paddleX = collision.transform.position.x;
            float relativeX = transform.position.x - paddleX;
            float normalized = relativeX / (paddleWidth / 2f);
            float maxAngle = 45f;
            float angle = normalized * maxAngle * Mathf.Deg2Rad;
            direction.x = Mathf.Sin(angle);
            direction.y = Mathf.Abs(Mathf.Cos(angle));
            direction = direction.normalized;
        }
        else if (collision.gameObject.CompareTag("Wall")) {
            direction.x *= -1;
        }
        else if (collision.gameObject.CompareTag("Ceiling")) {
            direction.y *= -1;
            if(!paddle.isShortened()) paddle.shorten();
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            gameManager.decreaseHP();
            if(gameManager.getHP() <= 0) gameManager.resetGame();  
            StartCoroutine(resetBall());
        }
    }

    public void callResetBall() {
        StartCoroutine(resetBall());
    }

    IEnumerator resetBall() {
        speed = 0;
        transform.position = Vector3.zero;
        if (paddle.isShortened()) paddle.unshorten();
        yield return waitFor1Second;
        gameManager.resetLifeBar();
        speed = INITIAL_SPEED;
        direction = Vector2.down;
        direction.x = Random.Range(-0.45f, 0.45f);
        direction.Normalize();
    }

}
