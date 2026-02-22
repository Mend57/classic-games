using UnityEngine;

public class Barrier : MonoBehaviour
{
    [SerializeField] private int health = 4;

    public void decreaseHealth() {
        if(--health <= 0 ) Destroy(gameObject);
    }
}
