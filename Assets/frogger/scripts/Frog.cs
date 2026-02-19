using UnityEngine;

public class Frog : MonoBehaviour {
    [SerializeField] private float moveDistance = 1f, moveSpeed = 5f;
    private bool isMoving = false, isInCooldown = false, isTeleporting = false;
    private Vector3 targetPosition;
    private const float COOLDOWN = 0.08f, TELEPORT_COOLDOWN = 0.5f;
    private float currentCooldown, currentTeleportCooldown;
    private static int highScore = 0;
    [SerializeField] private GameObject highScoreSound, moveSound;
    [SerializeField] private int score = 0;
    [SerializeField] private float minX, maxX, minY, maxY;


    private void Awake() {
        currentCooldown = COOLDOWN;
        currentTeleportCooldown = TELEPORT_COOLDOWN;
    }

    private void Update() {
        if (isTeleporting) manageTeleportCooldown();
        else if (isInCooldown) manageCooldown();
        else if (!isMoving) getInput();
        else move();
    }

    private void getInput() {
        Vector3 input = Vector3.zero;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            input = Vector3.up;
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            input = Vector3.down;
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            input = Vector3.left;
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            input = Vector3.right;

        if (input != Vector3.zero) {
            Vector3 proposedTarget = transform.position + input * moveDistance;
            proposedTarget.x = Mathf.Clamp(proposedTarget.x, minX, maxX);
            proposedTarget.y = Mathf.Clamp(proposedTarget.y, minY, maxY);
            targetPosition = proposedTarget; isMoving = true;
            moveSound.GetComponent<AudioSource>().Play();
        }
    }

    private void manageCooldown() {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0) {
            isMoving = false;
            isInCooldown = false;
            currentCooldown = COOLDOWN;

        }
    }

    private void manageTeleportCooldown() {
        currentTeleportCooldown -= Time.deltaTime;
        if (currentTeleportCooldown <= 0) {
            isTeleporting = false;
            currentTeleportCooldown = TELEPORT_COOLDOWN;
            currentCooldown = COOLDOWN;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    private void move() {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f) {
            transform.position = targetPosition;
            isInCooldown = true;
        }
    }

    public static int getHighScore() {
        return highScore;
    }

    public int getScore() {
        return score;
    }

    public void resetScore() {
        score = 0;
    }

    public void incrementScore() {
        score++;
    }

    public void updateHighScore() {
        if (score > highScore) {
            highScore = score;
            highScoreSound.GetComponent<AudioSource>().Play();
        }
    }

    public void setTeleporting() {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        isTeleporting = true;
        isMoving = false;
    }
}



