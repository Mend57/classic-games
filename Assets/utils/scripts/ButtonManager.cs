using UnityEngine.UI;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{

    private void Awake() {
        gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick() {
        AudioManager.Instance.PlaySound();
    }
}
