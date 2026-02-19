using UnityEngine;

public class Car : MonoBehaviour
{
    public static float speed;
    public const float MULTIPLIER = 0.1f;
    private Vector3 direction;

    private void Start() {
        Destroy(gameObject, 30f);
    }

    public void init(Vector3 _direction) {
        direction = _direction;
    }

    private void Update() { 
        transform.position += speed * Time.deltaTime * direction; 
    }

    public static void resetSpeed() {
        speed = 1f;
    }

    public static void increaseSpeed() {
        speed += speed * MULTIPLIER;
    }
}
