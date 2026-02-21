using UnityEngine;
using System;

public class Ship : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] GameObject gameManagerObj;
    private GameObject projectile;
    public event Action playerHit;
    private SpaceInvadersManager gameManager;

    void Awake() {
        gameManager = gameManagerObj.GetComponent<SpaceInvadersManager>();
        projectile = gameManager.playerProjectile;

    }

    private void Update() {
        if(gameManager.spawned){
            float input = Input.GetAxisRaw("Horizontal");
            if (!gameManager.blockMovement || input != gameManager.limitDirection) {
                transform.Translate(input * speed * Time.deltaTime * Vector3.right);
            }
            if (Input.GetKey(KeyCode.Space) && !gameManager.isPlayerProjectileActive) {
                shoot();
                gameManager.isPlayerProjectileActive = true;
            }
        }
    }

    private void shoot() {
        Instantiate(projectile, transform.position, Quaternion.identity);
        gameManager.playShotSound(projectile);
    }

    public void callPlayerGotHit() {
        playerHit?.Invoke();
    }
}
