using System.Collections.Generic;
using UnityEngine;

public class SpaceInvadersManager : MonoBehaviour {
    private const float ALIEN_SPEED_MULTIPLIER = 0.01f, ALIEN_SHOOT_COOLDOWN_MULTIPLIER = 1f;
    public float moveCooldown = 1f, shootCooldownLimit = 60f, mapLimit = 4.5f, spawnUFOCooldownLimit = 60f;
    private float spawnUFOCooldown;
    private const float MOVE_COOLDOWN_THRESHOLD = 0.1f, ALIEN_SHOOT_COOLDOWN_THRESHOLD = 5f;
    public int score = 0;
    public GameObject slowProjectile, normalProjectile, fastProjectile, playerProjectile;
    public GameObject highScoreSound, playerDeathSound, alienDeathSound, playerProjectileSound, alienProjectileSound;
    public GameObject player, UIManager, UFO;
    public GameObject aliensPrefab;
    public static int highScore = 0;
    public bool movingDown = false;
    private int topAliensSignalsReceived = 0;
    private List<GameObject> topAliens = new(), aliens = new();
    public bool isPlayerProjectileActive = false;
    private bool hasPlayedHighScoreSound = false;
    private float baseMoveCooldown, baseShootCooldownLimit;
    private Vector3 basePlayerPos;

    private void spawnAliens() {
        Instantiate(aliensPrefab, aliensPrefab.transform.position, Quaternion.identity);
        foreach(GameObject topAlien in GameObject.FindGameObjectsWithTag("TopAlien")){
            topAliens.Add(topAlien);
            aliens.Add(topAlien);
            topAlien.GetComponent<TopAlien>().died += handleDeath;
            topAlien.GetComponent<TopAlien>().finishedMovingDown += handleMoveDown;
        }
        foreach (GameObject alien in GameObject.FindGameObjectsWithTag("Enemy")) {
            aliens.Add(alien);
            alien.GetComponent<Alien>().died += handleDeath;
        }
        player.SetActive(true);
    }

    private void Update() {
        spawnUFOCooldown -= Time.deltaTime;
        if (spawnUFOCooldown <= 0) {
            spawnUFO();
            spawnUFOCooldown = Random.Range(5f, spawnUFOCooldownLimit + 1);
        }
    }

    private void OnEnable() {
        player.GetComponent<Ship>().playerDeath += handlePlayerDeath;
    }

    private void OnDisable() {
        player.GetComponent<Ship>().playerDeath -= handlePlayerDeath;
        foreach (GameObject topAlien in topAliens) {
            topAlien.GetComponent<TopAlien>().finishedMovingDown -= handleMoveDown;
        }
        foreach (GameObject alien in aliens) {
            alien.GetComponent<Alien>().died -= handleDeath;
        }
    }

    private void Awake() {
        baseMoveCooldown = moveCooldown;
        baseShootCooldownLimit = shootCooldownLimit;
        basePlayerPos = player.transform.position;
        player.SetActive(false);
    }

    private void Start() {
        spawnAliens();
        spawnUFOCooldown = Random.Range(5f, spawnUFOCooldownLimit + 1);
    }

    private void increaseSpeed() {
        moveCooldown = (moveCooldown - ALIEN_SPEED_MULTIPLIER >= MOVE_COOLDOWN_THRESHOLD) ? moveCooldown -= ALIEN_SPEED_MULTIPLIER : MOVE_COOLDOWN_THRESHOLD;
    }

    private void increaseFireRate() {
        shootCooldownLimit = (shootCooldownLimit - ALIEN_SHOOT_COOLDOWN_MULTIPLIER >= ALIEN_SHOOT_COOLDOWN_THRESHOLD) ? shootCooldownLimit -= ALIEN_SHOOT_COOLDOWN_MULTIPLIER : ALIEN_SHOOT_COOLDOWN_THRESHOLD;
    }

    private void handleMoveDown() {
        if (topAliensSignalsReceived < topAliens.Count) {
            topAliensSignalsReceived++;
        }
        else {
            movingDown = false;
            topAliensSignalsReceived = 0;
        }
    }

    private void handleDeath(GameObject deadAlien, int value) {
        alienDeathSound.GetComponent<AudioSource>().Play();
        aliens.Remove(deadAlien);
        if (deadAlien.CompareTag("TopAlien")) topAliens.Remove(deadAlien);
        increaseScore(value);
        Destroy(deadAlien);
        increaseSpeed();
        increaseFireRate();
        if (aliens.Count <= 0) restartGame();
    }

    private void handlePlayerDeath() {
        playerDeathSound.GetComponent<AudioSource>().Play();
        Destroy(player);
        UIManager.GetComponent<SpaceInvadersUIManager>().gameOver();
    }

    public void playShotSound(GameObject projectile) {
        if (projectile.GetComponent<PlayerProjectile>() != null) {
            playerProjectileSound.GetComponent<AudioSource>().Play();
        }
        else {
            alienProjectileSound.GetComponent<AudioSource>().Play();
        }

    }

    public void increaseScore(int value) {
        score += value;
        UIManager.GetComponent<SpaceInvadersUIManager>().setScore();
        if (score > highScore) {
            highScore = score;
            if (!hasPlayedHighScoreSound) {
                highScoreSound.GetComponent<AudioSource>().Play();
                hasPlayedHighScoreSound = true;
            }
        }
    }

    private void spawnUFO() {
        Instantiate(UFO, new Vector3(-10f, 3.3f, 0f), Quaternion.identity);
    }

    private void restartGame() {
        player.SetActive(false);
        spawnAliens();
        baseMoveCooldown /= 2;
        moveCooldown = baseMoveCooldown;
        baseShootCooldownLimit /= 2;
        shootCooldownLimit = baseShootCooldownLimit;
        player.transform.position = basePlayerPos;

    }
}
