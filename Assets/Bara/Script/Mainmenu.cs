using UnityEngine;
using UnityEngine.SceneManagement; // untuk ganti scene
using System.Collections; // untuk IEnumerator

public class MainMenu : MonoBehaviour
{
    public AudioSource clickSound; // drag file AudioSource di Inspector

    // Fungsi untuk tombol Play
    public void PlayGame()
    {
        StartCoroutine(PlaySoundAndChangeScene("Level"));
    }

    // Fungsi untuk tombol Exit
    public void ExitGame()
    {
        StartCoroutine(PlaySoundAndExit());
    }

    private IEnumerator PlaySoundAndChangeScene(string MainMenu)
    {
        clickSound.Play();
        yield return new WaitForSeconds(clickSound.clip.length); // tunggu sampai sound selesai
        SceneManager.LoadScene(MainMenu);
    }

    private IEnumerator PlaySoundAndExit()
    {
        clickSound.Play();
        yield return new WaitForSeconds(clickSound.clip.length);

        Debug.Log("Keluar dari game...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
