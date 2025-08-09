using UnityEngine;

public class BoxToggle : MonoBehaviour
{
    private GameObject closedBox;
    private GameObject openBox;
    private bool isOpened = false;

    private void Awake()
    {
        // Cari child bernama "ClosedBox" dan "OpenBox"
        closedBox = transform.Find("ClosedBox").gameObject;
        openBox = transform.Find("OpenBox").gameObject;

        // Pastikan awalnya tertutup
        closedBox.SetActive(true);
        openBox.SetActive(false);
    }

    public void OnBoxClick()
    {
        if (isOpened) return;

        closedBox.SetActive(false);
        openBox.SetActive(true);
        isOpened = true;
    }
}
