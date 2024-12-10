using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayCompetitive()
    {
        SceneManager.LoadScene("CompetitiveScene");
    }

    public void PlayCasual()
    {
        SceneManager.LoadScene("CasualScene");
    }

    public void PlayWithBot()
    {
        SceneManager.LoadScene("BotScene");
    }

    public void PlayLocal()
    {
        SceneManager.LoadScene("LocalScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}