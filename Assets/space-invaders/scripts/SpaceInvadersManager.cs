using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class SpaceInvadersManager : MonoBehaviour {
    private const float ALIEN_SPEED_MULTIPLIER = 0.01f, ALIEN_SHOOT_COOLDOWN_MULTIPLIER = 1f;
    public float moveCooldown = 1f, shootCooldownLimit = 60f, mapLimit = 1000000000000f, spawnUFOCooldownLimit = 60f;
    private float spawnUFOCooldown, baseMoveCooldown, baseShootCooldownLimit, newMoveCooldown;
    private const float MOVE_AMOUNT = 0.2f, MOVE_DOWN_AMOUNT = 0.3f;
    private const float MOVE_COOLDOWN_THRESHOLD = 0.1f, ALIEN_SHOOT_COOLDOWN_THRESHOLD = 5f;
    public int score = 0;
    public GameObject slowProjectile, normalProjectile, fastProjectile, playerProjectile;
    public GameObject highScoreSound, playerDeathSound, alienDeathSound, playerProjectileSound, alienProjectileSound, BGM;
    public GameObject player, UIManager, UFO;
    public GameObject lowerAliensPrefab, middleAliensPrefab, topAliensPrefab;
    private GameObject lowerAliensInstance, middleAliensInstance, topAliensInstance;
    public static int highScore = 0;
    private List<GameObject> aliens = new();
    public bool isPlayerProjectileActive = false, hasPlayedHighScoreSound = false, spawned = false, finishedMoving = true;
    private Vector3 basePlayerPos, direction = Vector3.right;
    private readonly WaitForSeconds spawnDelay = new(0.5f);
    private WaitForSeconds moveDelay = new(0.1f);
    [SerializeField] private int life = 3;
    public bool limitReached = false, firstLimitReached = false, blockMovement = false;
    public float limitDirection;

    private IEnumerator spawnAliens() {
        BGM.GetComponent<AudioSource>().Stop();
        yield return spawnDelay;
        lowerAliensInstance = Instantiate(lowerAliensPrefab, lowerAliensPrefab.transform.position, Quaternion.identity);
        yield return spawnDelay;
        middleAliensInstance = Instantiate(middleAliensPrefab, middleAliensPrefab.transform.position, Quaternion.identity);
        yield return spawnDelay;
        topAliensInstance = Instantiate(topAliensPrefab, topAliensPrefab.transform.position, Quaternion.identity);
        yield return spawnDelay;
        player.transform.position = basePlayerPos;
        player.SetActive(true);
        yield return spawnDelay;
        BGM.GetComponent<AudioSource>().Play();
        foreach (Transform child in lowerAliensInstance.transform) {
            aliens.Add(child.gameObject);
            child.GetComponent<Alien>().died += handleDeath;
        }
        foreach (Transform child in middleAliensInstance.transform) {
            aliens.Add(child.gameObject);
            child.GetComponent<Alien>().died += handleDeath;
        }
        foreach (Transform child in topAliensInstance.transform) {
            aliens.Add(child.gameObject);
            child.GetComponent<Alien>().died += handleDeath;
        }
        spawned = true;
    }

    private IEnumerator move() {
        if (!limitReached || firstLimitReached) {
            if (lowerAliensInstance.transform.childCount > 0) lowerAliensInstance.transform.position += direction * MOVE_AMOUNT;
            yield return moveDelay;
            if (middleAliensInstance.transform.childCount > 0) middleAliensInstance.transform.position += direction * MOVE_AMOUNT;
            yield return moveDelay;
            if (topAliensInstance.transform.childCount > 0) topAliensInstance.transform.position += direction * MOVE_AMOUNT;
            limitReached = false;
            firstLimitReached = false;
        }
        else {
            firstLimitReached = true;
            if (lowerAliensInstance.transform.childCount > 0) lowerAliensInstance.transform.position += Vector3.down * MOVE_DOWN_AMOUNT;
            yield return moveDelay;
            if (middleAliensInstance.transform.childCount > 0) middleAliensInstance.transform.position += Vector3.down * MOVE_DOWN_AMOUNT;
            yield return moveDelay;
            if (topAliensInstance.transform.childCount > 0) topAliensInstance.transform.position += Vector3.down * MOVE_DOWN_AMOUNT;
            direction = direction == Vector3.right ? Vector3.left : Vector3.right;
        }
        finishedMoving = true;
    }

    private void Update() {
        if (spawned) {
            moveCooldown -= Time.deltaTime;
            if (moveCooldown <= 0 && finishedMoving) {
                finishedMoving = false;
                StartCoroutine(move());
                moveCooldown = newMoveCooldown;
            }
            spawnUFOCooldown -= Time.deltaTime;
            if (spawnUFOCooldown <= 0) {
                spawnUFO();
                spawnUFOCooldown = Random.Range(5f, spawnUFOCooldownLimit + 1);
            }
        }
    }

    private void OnEnable() {
        player.GetComponent<Ship>().playerHit += handlePlayerHit;
    }

    private void OnDisable() {
        player.GetComponent<Ship>().playerHit -= handlePlayerHit;
        foreach (GameObject alien in aliens) {
            alien.GetComponent<Alien>().died -= handleDeath;
        }
    }

    private void Awake() {
        baseMoveCooldown = moveCooldown;
        newMoveCooldown = moveCooldown;
        baseShootCooldownLimit = shootCooldownLimit;
        basePlayerPos = player.transform.position;
        player.SetActive(false);
    }

    private void Start() {
        StartCoroutine(spawnAliens());
        spawnUFOCooldown = Random.Range(5f, spawnUFOCooldownLimit + 1);
    }

    private void increaseSpeed() {
        newMoveCooldown = Mathf.Max(newMoveCooldown - ALIEN_SPEED_MULTIPLIER, MOVE_COOLDOWN_THRESHOLD);
        moveCooldown = Mathf.Min(moveCooldown, newMoveCooldown);
    }

    private void increaseFireRate() {
        shootCooldownLimit = Mathf.Max(shootCooldownLimit - ALIEN_SHOOT_COOLDOWN_MULTIPLIER, ALIEN_SHOOT_COOLDOWN_THRESHOLD);
    }

    private void handleDeath(GameObject deadAlien, int value) {
        alienDeathSound.GetComponent<AudioSource>().Play();
        aliens.Remove(deadAlien);
        deadAlien.GetComponent<Alien>().died -= handleDeath;
        increaseScore(value);
        Destroy(deadAlien);
        increaseSpeed();
        increaseFireRate();
        if (aliens.Count <= 0) restartGame();
    }

    private void handlePlayerHit() {
        playerDeathSound.GetComponent<AudioSource>().Play();
        if (--life <= 0) {
            player.GetComponent<Ship>().playerHit -= handlePlayerHit;
            SpecialShip[] UFOsInMap = FindObjectsByType<SpecialShip>(FindObjectsSortMode.None);
            foreach (SpecialShip UFOInMap in UFOsInMap) UFOInMap.gameObject.GetComponent<AudioSource>().Stop();
            Destroy(player);
            UIManager.GetComponent<SpaceInvadersUIManager>().gameOver();
        }
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
        Instantiate(UFO, new Vector3(-9.5f, 3.65f, 0f), Quaternion.identity);
    }

    private void restartGame() {
        spawned = false;
        limitReached = false;
        firstLimitReached = false;
        blockMovement = false;
        player.SetActive(false);
        finishedMoving = true;
        isPlayerProjectileActive = false;
        direction = Vector3.right;
        baseMoveCooldown /= 2;
        moveCooldown = baseMoveCooldown;
        newMoveCooldown = moveCooldown;
        baseShootCooldownLimit /= 2;
        shootCooldownLimit = baseShootCooldownLimit;
        spawnUFOCooldown = Random.Range(5f, spawnUFOCooldownLimit + 1);
        StartCoroutine(spawnAliens());
    }

    public void callGameOver() {
        player.GetComponent<Ship>().playerHit -= handlePlayerHit;
        SpecialShip[] UFOsInMap = FindObjectsByType<SpecialShip>(FindObjectsSortMode.None);
        foreach (SpecialShip UFOInMap in UFOsInMap) Destroy(UFOInMap.gameObject);
        Projectile[] projectilesInMap = FindObjectsByType<Projectile>(FindObjectsSortMode.None);
        foreach (Projectile projectileInMap in projectilesInMap) Destroy(projectileInMap.gameObject);
        Destroy(lowerAliensInstance);
        Destroy(middleAliensInstance);
        Destroy(topAliensInstance);
        Destroy(player);
        UIManager.GetComponent<SpaceInvadersUIManager>().gameOver();
    }
}
