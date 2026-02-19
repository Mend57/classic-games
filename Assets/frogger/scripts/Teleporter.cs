using UnityEngine;

public class Teleporter : MonoBehaviour
{
    private const float INITIAL_Y_POS = -4f;
    private GameObject _UIManager;
    [SerializeField] private GameObject deathSound, scoreSound;


    private void Awake() {
        _UIManager = GameObject.FindGameObjectWithTag("UIManager");
    }

    void OnTriggerEnter2D(Collider2D collision) {
        GameObject player = collision.gameObject;
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");
        if (gameObject.CompareTag("Car")) {
            deathSound.GetComponent<AudioSource>().Play();
            player.GetComponent<Frog>().resetScore();
            Car.resetSpeed();
            foreach(GameObject spawner in spawners) spawner.GetComponent<CarSpawner>().setInterval();
            player.transform.position = new Vector3(0, INITIAL_Y_POS, 0);
        }
        else {
            player.GetComponent<Frog>().incrementScore();
            if (player.GetComponent<Frog>().getScore() <= Frog.getHighScore()) scoreSound.GetComponent<AudioSource>().Play();
            player.GetComponent<Frog>().updateHighScore();
            Car.increaseSpeed();
            foreach (GameObject spawner in spawners) spawner.GetComponent<CarSpawner>().setInterval();
            player.transform.position = new Vector3(player.transform.position.x, INITIAL_Y_POS, 0);
        }
        _UIManager.GetComponent<FroggerUIManager>().setScore();
        player.GetComponent<Frog>().setTeleporting();
    }
}
