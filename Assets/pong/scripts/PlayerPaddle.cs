using UnityEngine;

public class PlayerPaddle : MonoBehaviour {
    private Camera cam;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    void Awake() {
        cam = Camera.main;
    }

    void Update() {
        if(Time.timeScale != 0f){
            Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
            float clampedY = Mathf.Clamp(mouseWorld.y, minY, maxY);
            transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
        }
    }
}
