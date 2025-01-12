using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private PlayerLogin Cliente;
    private void Start()
    {
        Cliente = GetComponent<PlayerLogin>();
    }

    public void PlayGame()
    {
        Debug.Log("Carregando a cena do jogo...");
        SceneManager.LoadScene("CompetitiveScene");
    }

    public void QuitGame()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();
    }
}