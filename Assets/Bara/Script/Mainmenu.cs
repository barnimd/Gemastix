using UnityEngine;
using UnityEngine.SceneManagement; // untuk ganti scene

public class MainMenu : MonoBehaviour
{
    // Fungsi untuk tombol Play
    public void PlayGame()
    {
        // Ganti "GameScene" dengan nama scene gameplay kamu
        SceneManager.LoadScene("Level");
    }

    // Fungsi untuk tombol Exit
    public void ExitGame()
    {
        Debug.Log("Keluar dari game...");

        // Keluar dari aplikasi
        Application.Quit();

        // Kalau sedang di Unity Editor, ini untuk menghentikan Play Mode
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
