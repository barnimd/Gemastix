using UnityEngine;
using UnityEngine.UI;
using TMPro;    

public class TabletDisplayController : MonoBehaviour
{
    public GameObject tabletPanel;      // Panel UI tablet yang mau ditampilkan
    public TMP_Text dataText;               // Text UI untuk menampilkan data (bisa diganti dengan TMP_Text)

    private void Start()
    {
        tabletPanel.SetActive(false);   // Sembunyikan panel tablet di awal
    }

    public void OnImageClicked()
    {
        // Tampilkan panel tablet
        tabletPanel.SetActive(true);

        // Ambil data dari GameManagerUI atau tempat lain
        string dataToShow = "Data belum tersedia";

        if (GameManagerUI.Instance != null)
        {
            string scannedData = GameManagerUI.Instance.scannedIconDataString;
            if (!string.IsNullOrEmpty(scannedData))
            {
                dataToShow = scannedData;
            }
            else
            {
                dataToShow = "Belum ada data scan.";
            }
        }

        dataText.text = dataToShow;
    }

    public void CloseTablet()
    {
        tabletPanel.SetActive(false);
    }
}
