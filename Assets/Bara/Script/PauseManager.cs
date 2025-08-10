using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel; // assign panel pause di inspector
    public AudioSource clickSound; // drag AudioSource ke sini
    private bool isPaused = false;

    void Update()
    {
        // Kalau ada shortcut pause bisa ditaruh di sini
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f; // Hentikan waktu
        isPaused = true;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f; // Lanjutkan waktu
        isPaused = false;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Pastikan waktu normal
        StartCoroutine(PlaySoundAndGoToMainMenu());
    }

    private IEnumerator PlaySoundAndGoToMainMenu()
    {
        if (clickSound != null)
        {
            clickSound.Play();
            yield return new WaitForSeconds(clickSound.clip.length);
        }

        SceneManager.LoadScene("MainMenu"); // Ganti dengan nama scene Main Menu kamu
    }
}
