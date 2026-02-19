using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject car;
    [SerializeField] private Vector3 direction;
    [SerializeField] private float desiredSpacing = 6f;
    [SerializeField] private int initialCars = 3;
    private float timer, spawnInterval;

    private void Awake() {
        Car.resetSpeed();
        spawnInterval = desiredSpacing / Car.speed;

        for (int i = 0; i < initialCars; i++) {
            Vector3 spawnPos = transform.position + direction * (i * desiredSpacing);
            GameObject cars = Instantiate(car, spawnPos, Quaternion.identity);
            foreach (Car c in cars.GetComponentsInChildren<Car>()) c.init(direction);
        }
        timer = spawnInterval;
    }

    private void Update() {
        timer -= Time.deltaTime;
        while (timer <= 0) {
            GameObject cars = Instantiate(car, transform.position, Quaternion.identity);
            foreach (Car c in cars.GetComponentsInChildren<Car>()) c.init(direction);
            timer += desiredSpacing / Car.speed;
        }
    }
    public void setInterval() {
        float oldInterval = spawnInterval;
        spawnInterval = desiredSpacing / Car.speed;
        timer *= (spawnInterval / oldInterval);
    }
}
