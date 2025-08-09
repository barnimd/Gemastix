using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LeverSwitch : MonoBehaviour
{
    [Header("Lever Images")]
    public GameObject leverDown;
    public GameObject leverUp;

    [Header("Delay Settings")]
    public float resetDelay = 6f;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnLeverClicked);
        }
    }

    private void Start()
    {
        // Pastikan posisi awal
        SetLeverState(true);
    }

    private void OnLeverClicked()
    {
        SetLeverState(false); // leverDown OFF, leverUp ON
        StartCoroutine(ResetLeverAfterDelay());
    }

    private IEnumerator ResetLeverAfterDelay()
    {
        yield return new WaitForSeconds(resetDelay);
        SetLeverState(true); // leverDown ON, leverUp OFF
    }

    private void SetLeverState(bool isDown)
    {
        if (leverDown != null) leverDown.SetActive(isDown);
        if (leverUp != null) leverUp.SetActive(!isDown);
    }
}
