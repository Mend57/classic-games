using UnityEngine;

public class PlayerProjectile : Projectile
{
    private SpaceInvadersManager gameManager;

    private void Awake()
    {
        direction = Vector3.up;
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpaceInvadersManager>();
    }

    protected override void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.CompareTag("Player")) {
            if (collision.CompareTag("Wall")) {
                collision.GetComponent<Barrier>().decreaseHealth();
            }
            else if (collision.CompareTag("Enemy") || collision.CompareTag("TopAlien")) {
                collision.GetComponent<Alien>().callAlienDeath();
            }
            gameManager.isPlayerProjectileActive = false;
            Destroy(gameObject);
        }
    }
}
