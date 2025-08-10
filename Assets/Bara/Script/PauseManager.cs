using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel; // assign panel pause di inspector
    private bool isPaused = false;

    void Update()
    {
      
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
        Time.timeScale = 1f; // Pastikan waktu normal sebelum ganti scene
        SceneManager.LoadScene("MainMenu"); // Ganti dengan nama scene Main Menu kamu
    }
}
