using UnityEngine;
using System.Collections;

public class BoxUIController : MonoBehaviour
{
    public float moveSpeed = 500f;
    private RectTransform rect;
    private bool isOpened = false;

    [Header("Logo Settings")]
    public RectTransform logoPrefab;
    public Vector2 logoOffset = new Vector2(0, 150);
    public RectTransform logo;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void MoveToCenter(Vector2 targetPos)
    {
        StartCoroutine(MoveRoutine(targetPos));
    }

    IEnumerator MoveRoutine(Vector2 targetPos)
    {
        while (Vector2.Distance(rect.anchoredPosition, targetPos) > 1f)
        {
            rect.anchoredPosition = Vector2.MoveTowards(rect.anchoredPosition, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        rect.anchoredPosition = targetPos;
    }

    public void OnBoxClicked()
    {
        if (isOpened) return;
        isOpened = true;

        // Spawn logo sebagai child box supaya ikut posisi & hilang bersama box
        logo = Instantiate(logoPrefab, rect);
        logo.anchoredPosition = logoOffset;

        // Random hasil logo (benar/salah)
        GameManagerUI.Instance.RandomizeLogoResult();
    }


    public void RemoveBox()
    {
        StartCoroutine(RemoveBoxAnimation());
    }
    private IEnumerator RemoveBoxAnimation()
    {
        // Animasi jatuh
        float duration = 0.5f;
        Vector3 startPos = transform.localPosition;
        Vector3 endPos = startPos + new Vector3(0, -500, 0);

        float time = 0;
        while (time < duration)
        {
            transform.localPosition = Vector3.Lerp(startPos, endPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject); // ini otomatis hapus box + logo (kalau child)

        GameManagerUI.Instance.BoxProcessFinished();
    }
    public void MoveToMachineIn()
    {
        StartCoroutine(MoveRightRoutine());
    }

    IEnumerator MoveRightRoutine()
    {
        Vector2 target = GameManagerUI.Instance.centerPoint.anchoredPosition + new Vector2(800, 0);
        while (Vector2.Distance(rect.anchoredPosition, target) > 1f)
        {
            rect.anchoredPosition = Vector2.MoveTowards(rect.anchoredPosition, target, moveSpeed * Time.deltaTime);
            yield return null;
        }
        RemoveBox();

        GameManagerUI.Instance.BoxProcessFinished();
    }
}
