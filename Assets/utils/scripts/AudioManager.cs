using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager Instance;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
            return;
        }
    }

    public void PlaySound() {
        gameObject.GetComponent<AudioSource>().Play();
    }
}
