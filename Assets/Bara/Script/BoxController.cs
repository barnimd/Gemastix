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

        // Pilih random prefab icon dari GameManagerUI
        int idx = Random.Range(0, GameManagerUI.Instance.iconPrefabs.Count);
        GameObject iconPrefab = GameManagerUI.Instance.iconPrefabs[idx];
        // Spawn icon sebagai child box
        GameObject iconObj = Instantiate(iconPrefab, rect);
        RectTransform iconRect = iconObj.GetComponent<RectTransform>();
        iconRect.anchoredPosition = logoOffset;

        // Simpan data icon hasil spawn ke GameManagerUI supaya bisa diakses nanti
        IconDataHolder dataHolder = iconObj.GetComponent<IconDataHolder>();
        if (dataHolder != null)
        {
            GameManagerUI.Instance.scannedIconDataString = dataHolder.GetFormattedData();
            GameManagerUI.Instance.currentLogoIsReal = dataHolder.isRealIcon;
        }
    }


    public void RemoveBox()
    {
        StartCoroutine(RemoveBoxAnimation());
    }
    private IEnumerator RemoveBoxAnimation()
    {
        // Animasi jatuh
        float duration = 2;
        Vector3 startPos = transform.localPosition;
        Vector3 endPos = startPos + new Vector3(-1500, 0, 0);

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
        Vector2 target = GameManagerUI.Instance.centerPoint.anchoredPosition + new Vector2(1100, 0);
        while (Vector2.Distance(rect.anchoredPosition, target) > 1f)
        {
            rect.anchoredPosition = Vector2.MoveTowards(rect.anchoredPosition, target, moveSpeed * Time.deltaTime);
            yield return null;
        }

        Destroy(gameObject);

        GameManagerUI.Instance.BoxProcessFinished();
    }
}
