using UnityEngine;
using System;

public abstract class Alien : MonoBehaviour
{
    [SerializeField] private float shootCooldown;
    protected GameObject projectile;
    protected int scoreValue;
    protected SpaceInvadersManager gameManager;
    public event Action<GameObject, int> died;

    protected virtual void Awake() {        
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpaceInvadersManager>();
        setProjectile();
        shootCooldown = UnityEngine.Random.Range(5f, gameManager.shootCooldownLimit + 1);
        setScoreValue();
    }

    protected virtual void Update()
    {
        if(gameManager.spawned){
            shootCooldown -= Time.deltaTime;
            if (shootCooldown <= 0) {
                shoot();
                shootCooldown = UnityEngine.Random.Range(5f, gameManager.shootCooldownLimit + 1);
            }
        }
    }

    private void shoot() {
        Instantiate(projectile, transform.position, Quaternion.identity);
        gameManager.playShotSound(projectile);
    }

    public void callAlienDeath() {
        died?.Invoke(gameObject, scoreValue);
    }

    protected abstract void setProjectile();
    protected abstract void setScoreValue();

}
