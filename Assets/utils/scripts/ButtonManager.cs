using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonManager : MonoBehaviour, IPointerUpHandler {

    private void Awake() {
        gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick() {
        AudioManager.Instance.PlaySound();
    }

    public void OnPointerUp(PointerEventData eventData) {
        EventSystem.current.SetSelectedGameObject(null);
    }
}
