using UnityEngine;
using UnityEngine.EventSystems;

public class LogoScanTarget : MonoBehaviour, IDropHandler
{
    private bool isScanned = false;

    public void OnDrop(PointerEventData eventData)
    {
        if (isScanned) return; // Sudah di-scan sebelumnya

        DragDropScanner scanner = eventData.pointerDrag.GetComponent<DragDropScanner>();
        if (scanner != null)
        {
            isScanned = true;
            Debug.Log("Logo berhasil di-scan, data dikirim ke tablet.");

            if (GameManagerUI.Instance != null)
            {
                GameManagerUI.Instance.EnableAcceptDeclineButtons();
            }
        }
    }
}
