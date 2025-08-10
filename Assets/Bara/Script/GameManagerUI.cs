using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManagerUI : MonoBehaviour
{
    public static GameManagerUI Instance;

    [Header("References")]
    public RectTransform boxPrefab;
    public RectTransform spawnPoint;
    public RectTransform centerPoint;
    public Button nextButton;
    public GameObject neonConveyor;
    public List<GameObject> iconPrefabs;  // Assign semua prefab icon asli + palsu di inspector
    public GameObject tabletPanel;
    public GameObject gameOverPanel;


    [Header("UI Gameplay")]
    public List<Image> lifeImages; // nyawa di kanan atas
    public TMP_Text scoreText;
    public Button acceptButton;
    public Button declineButton;

    private bool boxActive = false;
    private BoxUIController activeBox;
    private int lives = 3;
    private int score = 0;
    private bool isGameOver = false;

    [HideInInspector]
    public string scannedIconDataString = "";
    public bool currentLogoIsReal;


    private void Awake()
    {
        Instance = this;
        acceptButton.interactable = false;
        declineButton.interactable = false;
    }

    public void OnNextPressed()
    {
        if (boxActive) return;

        // Pastikan box di belakang tablet
        Transform tabletTransform = tabletPanel.transform; // tabletPanel = panel tablet kamu
        
        RectTransform newBox = Instantiate(boxPrefab, spawnPoint.position, Quaternion.identity, spawnPoint.parent);
        newBox.SetSiblingIndex(tabletTransform.GetSiblingIndex());
        activeBox = newBox.GetComponent<BoxUIController>();
        activeBox.MoveToCenter(centerPoint.anchoredPosition);

        boxActive = true;
        nextButton.interactable = false;
    }

    public void BoxProcessFinished()
    {
        boxActive = false;
        nextButton.interactable = true;
    }

    // Dipanggil saat logo muncul
    
    public void EnableAcceptDeclineButtons()
    {
        acceptButton.interactable = true;
        declineButton.interactable = true;
    }
    public void DisableAcceptDeclineButtons()
    {
        acceptButton.interactable = false;
        declineButton.interactable = false;
    }

    public void OnAcceptPressed()
    {
        if (activeBox == null) return;

        // disable tombol setelah dipakai, agar harus scan ulang
        DisableAcceptDeclineButtons();

        if (currentLogoIsReal)
        {
            score++;
            UpdateScore();
            activeBox.MoveToMachineIn();
        }
        else
        {
            LoseLife();
            activeBox.MoveToMachineIn();
        }
    }

    public void OnDeclinePressed()
    {
        if (activeBox == null) return;

        DisableAcceptDeclineButtons();

        // Matikan neon conveyor
        neonConveyor.SetActive(false);

        // Jalankan animasi box jatuh
        activeBox.RemoveBox();

        // Setelah 1 detik hidupkan lagi neon
        StartCoroutine(ShowNeonDelay(1f));

        // Cek benar/salah
        if (!currentLogoIsReal)
        {
            score++;
            UpdateScore();
        }
        else
        {
            LoseLife();
        }
    }
    private IEnumerator ShowNeonDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        neonConveyor.SetActive(true);
    }

    private void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
    public void RestartGame()
    {
        Time.timeScale = 1f; // kembalikan waktu
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // reload scene
    }
    private void LoseLife()
    {
        lives--;
        if (lives >= 0 && lives < lifeImages.Count)
        {
            lifeImages[lives].enabled = false; // hilangkan gambar nyawa terakhir
        }

        if (lives <= 0)
        {
            Time.timeScale = 0f; // hentikan waktu
            gameOverPanel.SetActive(true);
            isGameOver = true;
            Debug.Log("Game Over!");
            // TODO: buat panel game over
        }
    }
}
