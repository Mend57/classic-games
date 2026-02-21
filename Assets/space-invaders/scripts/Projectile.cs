using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected float speed;
    protected Vector3 direction = Vector3.down;

    protected virtual void Update() {
        transform.position += speed * Time.deltaTime * direction;
    }

    protected virtual void alienProjectileOnTrigger(Collider2D collision) {
        if (!collision.CompareTag("Enemy") && !collision.CompareTag("Projectile")) {
            if (collision.CompareTag("Wall")) {
                collision.GetComponent<Barrier>().decreaseHealth();
            }
            else if (collision.CompareTag("Player")) {
                collision.GetComponent<Ship>().callPlayerGotHit();
            }
            if (collision.GetComponent<GameOverLimit>() == null) Destroy(gameObject);
        }
    }
}
