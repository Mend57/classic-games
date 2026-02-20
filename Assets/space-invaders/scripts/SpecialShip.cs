using UnityEngine;

public class SpecialShip : MonoBehaviour {
    private Vector3 direction;
    [SerializeField] private float speed;
    private int value;
    SpaceInvadersManager gameManager;

    void Awake() {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpaceInvadersManager>();
        int[] values = { 50, 100, 150, 300 };
        value = values[Random.Range(0, values.Length)];
        Vector3[] directions = { Vector3.left, Vector3.right };
        direction = directions[Random.Range(0, directions.Length)];
        if (direction == Vector3.left) transform.position = new(-transform.position.x, transform.position.y, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<Projectile>() != null) gameManager.increaseScore(value);
        Destroy(gameObject);
    }


    void Update() {
        transform.position += speed * Time.deltaTime * direction;
    }
}
