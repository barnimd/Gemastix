using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropScanner : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rect;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Vector2 startPos;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        startPos = rect.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false; // Biar detect logo
    }

    public void OnDrag(PointerEventData eventData)
    {
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rect.anchoredPosition = startPos; // Kembali ke posisi awal
        canvasGroup.blocksRaycasts = true;
    }
}
