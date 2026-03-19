using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour {
    private static readonly WaitForSeconds waitFor1Second = new(1f);
    [SerializeField] private float speed;
    [SerializeField] private GameObject ballSound;
    private Rigidbody2D rb;
    private Vector2 direction;

    void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine(wait());
    }

    IEnumerator wait() {
        yield return waitFor1Second;
        resetBall();
    }

    void FixedUpdate() {
        rb.linearVelocity = direction * speed;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        ballSound.GetComponent<AudioSource>().Play();
        if (collision.gameObject.CompareTag("Player")) {
            float paddleHeight = collision.collider.bounds.size.y;
            float paddleY = collision.transform.position.y;
            float relativeY = transform.position.y - paddleY;
            float normalized = relativeY / (paddleHeight / 2f);
            float maxAngle = 45;
            float angle = normalized * maxAngle * Mathf.Deg2Rad;
            float directionX = direction.x > 0 ? -1 : 1;
            direction.x = directionX * Mathf.Cos(angle);
            direction.y = Mathf.Sin(angle);
            direction = direction.normalized;
        }
        if (collision.gameObject.CompareTag("Wall")) {
            direction.y *= -1;
        }
    }

    public void resetBall() {
        transform.position = Vector3.zero;
        direction = Random.value > 0.5f ? Vector2.left : Vector2.right;
        direction.y = Random.Range(-0.3f, 0.3f);
        direction.Normalize();
    }
}
