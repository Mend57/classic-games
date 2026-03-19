using UnityEngine;

public class EnemyPaddle : MonoBehaviour
{
    [SerializeField] private Transform ball;
    [SerializeField] private float speed;
    private Rigidbody2D rb;

    void Awake() {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        rb.MovePosition(new Vector2(rb.position.x, Mathf.MoveTowards(rb.position.y, ball.position.y, speed * Time.fixedDeltaTime)));
    }
}
