using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    // Funkcja do rozpoczêcia gry
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // Funkcja do zakoñczenia gry
    public void QuitGame()
    {
        // Zakoñczenie aplikacji
        Application.Quit();

        // Dla edytora Unity, aby wyjœæ z gry, u¿yj:
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}