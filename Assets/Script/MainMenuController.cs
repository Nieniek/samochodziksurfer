using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    // Funkcja do rozpocz�cia gry
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // Funkcja do zako�czenia gry
    public void QuitGame()
    {
        // Zako�czenie aplikacji
        Application.Quit();

        // Dla edytora Unity, aby wyj�� z gry, u�yj:
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}