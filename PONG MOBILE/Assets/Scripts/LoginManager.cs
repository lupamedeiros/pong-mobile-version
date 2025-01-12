using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public InputField playerNameInput;
    public Button playButton;

    private void Start()
    {
        playButton.onClick.AddListener(OnPlayButtonClicked);
        ClienteServer.Instance.ConnectToServer();
    }

    private void OnPlayButtonClicked()
    {
        string playerName = playerNameInput.text.Trim();
        if (!string.IsNullOrEmpty(playerName))
        {
            ClienteServer.Instance.SendPlayerName(playerName);
            SceneManager.LoadScene(1);
        }
        else
        {
            Debug.LogWarning("Nome do jogador não pode estar vazio.");
        }
    }
}