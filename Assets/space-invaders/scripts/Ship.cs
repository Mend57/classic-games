using UnityEngine;
using System;

public class Ship : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] GameObject gameManagerObj;
    private GameObject projectile;
    public event Action playerDeath;
    private SpaceInvadersManager gameManager;

    void Awake() {
        gameManager = gameManagerObj.GetComponent<SpaceInvadersManager>();
        projectile = gameManager.playerProjectile;

    }

    private void Update() {
        float input = Input.GetAxisRaw("Horizontal");
        transform.Translate(input * speed * Time.deltaTime * Vector3.right);
        if(Input.GetKey(KeyCode.Space) && !gameManager.isPlayerProjectileActive) {
            shoot();
            gameManager.isPlayerProjectileActive = true;  
        }
    }

    private void shoot() {
        Instantiate(projectile, transform.position, Quaternion.identity);
        gameManager.playShotSound(projectile);
    }

    public void callPlayerDeath() {
        playerDeath?.Invoke();
    }
}
