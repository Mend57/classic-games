using UnityEngine;
using System;

public abstract class Alien : MonoBehaviour
{
    [SerializeField] private float shootCooldown;
    protected GameObject projectile;
    private Vector3 direction = Vector3.right;
    protected float moveAmount = 0.2f;
    protected float _moveCooldown;
    protected int scoreValue;
    protected SpaceInvadersManager gameManager;
    public event Action<GameObject, int> died;

    protected virtual void Awake() {        
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpaceInvadersManager>();
        setProjectile();
        shootCooldown = UnityEngine.Random.Range(5f, gameManager.shootCooldownLimit + 1);
        setMoveCooldown();
        setScoreValue();
    }

    protected virtual void Update()
    {
        _moveCooldown -= Time.deltaTime;
        if(_moveCooldown <= 0) {
            move();
            setMoveCooldown();
        }
        shootCooldown -= Time.deltaTime;
        if (shootCooldown <= 0) {
            shoot();
            shootCooldown = UnityEngine.Random.Range(5f, gameManager.shootCooldownLimit + 1);
        }
    }

    protected virtual void move() {
        Vector3 pos = transform.position;
        bool movingDown = gameManager.movingDown;
        if (movingDown) {
            pos.y -= 0.3f;
            changeDirection();
        }
        else if (Mathf.Abs(pos.x + moveAmount) <= gameManager.mapLimit) {
            pos.x += moveAmount;
        }
        else {
            if(movingDown == false) gameManager.movingDown = true;
        }
        transform.position = pos;
    }

    private void shoot() {
        Instantiate(projectile, transform.position, Quaternion.identity);
        gameManager.playShotSound(projectile);
    }

    protected void changeDirection() {
        direction = direction == Vector3.right ? Vector3.left : Vector3.right;
        moveAmount = -moveAmount;
    }

    public void callAlienDeath() {
        died?.Invoke(gameObject, scoreValue);
    }

    protected abstract void setProjectile();
    protected abstract void setMoveCooldown();
    protected abstract void setScoreValue();

}
