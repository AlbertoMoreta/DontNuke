using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableWindow : MonoBehaviour, IDragHandler {

    public Canvas canvas;
    private RectTransform rectTransform;

    void Start() {
        Debug.Log("Start Drggable");
        rectTransform = transform.parent.gameObject.GetComponent<RectTransform>();
    }

    void IDragHandler.OnDrag(PointerEventData eventData) {
        Debug.Log("Dragging");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

}
