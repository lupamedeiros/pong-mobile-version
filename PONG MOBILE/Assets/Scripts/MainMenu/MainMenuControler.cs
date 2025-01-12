using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Text Name;
    public Text id;
    public Text score;

    private void Update()
    {
        Name.text = "Nome: " + ClienteServer.Instance.PlayerName;
        id.text = "ID: " + ClienteServer.Instance.PlayerId.ToString();
        score.text = "Score: " + ClienteServer.Instance.PlayerScore.ToString();
    }

    public void PlayGame()
    {
        Debug.Log("Carregando a cena do jogo...");
        SceneManager.LoadScene("CompetitiveScene");
    }

    public void PlayBot()
    {
        SceneManager.LoadScene("BotScene");
    }

    public void QuitGame()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();
    }
}