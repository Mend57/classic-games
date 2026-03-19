using UnityEngine;

public class Paddle : MonoBehaviour {
    private Camera cam;
    private bool shortened = false;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;

    void Awake() {
        cam = Camera.main;
    }

    void Update() {
        if (Time.timeScale != 0f) {
            Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
            float clampedX = Mathf.Clamp(mouseWorld.x, minX, maxX);
            transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
        }
    }

    public bool isShortened() {
        return shortened;
    }

    public void shorten() {
        Vector3 scale = gameObject.transform.localScale;
        scale.x /= 2;
        gameObject.transform.localScale = scale;
        minX -= 0.13f;
        maxX += 0.13f;
        shortened = true;
    }

    public void unshorten() {
        Vector3 scale = gameObject.transform.localScale;
        scale.x *= 2;
        minX += 0.13f;
        maxX -= 0.13f;
        gameObject.transform.localScale = scale;
        shortened = false;
    }
}