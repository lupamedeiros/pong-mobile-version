using System;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerLogin : MonoBehaviour
{
   /* // Variáveis para conexão com o servidor
    private TcpClient client;
    private NetworkStream stream;
    private string serverAddress = "127.0.0.1"; // Endereço do servidor
    private int port = 13000; // Porta do servidor

    // Variáveis do jogador
    public static string playerName = "Jogador";  // Nome do jogador
    public static int playerScore = 0;  // Pontuação do jogador

    // Referência ao InputField e ao Button no Canvas de Login
    public InputField playerNameInputField;
    public Button loginButton;

    // Referência ao Text na Cena MainMenu (para mostrar nome e score)
    public Text playerInfoText;

    // Singleton instance
    public static PlayerLogin Instance;

    // Inicia a conexão com o servidor e configura o botão de login
    void Start()
    {
        // Se já existir uma instância, destruir o GameObject atual (para garantir um único singleton)
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // Faz o script persistir entre cenas

        // Conectar ao servidor
        ConnectToServer();

        // Configurar o botão de login
        loginButton.onClick.AddListener(OnLoginButtonClick);
    }

    // Conecta-se ao servidor
    private void ConnectToServer()
    {
        try
        {
            client = new TcpClient(serverAddress, port);
            stream = client.GetStream();
            Debug.Log("Conectado ao servidor!");
        }
        catch (Exception e)
        {
            Debug.LogError("Erro de conexão: " + e.Message);
        }
    }

    // Método chamado quando o botão de login é clicado
    private void OnLoginButtonClick()
    {
        playerName = playerNameInputField.text;

        if (string.IsNullOrEmpty(playerName))
        {
            Debug.LogWarning("Nome não pode ser vazio.");
            return;
        }

        SendLoginRequest();
    }

    // Envia o nome do jogador ao servidor para verificar e criar, se necessário
    private void SendLoginRequest()
    {
        if (client != null && stream != null)
        {
            string data = $"login,{playerName}";
            byte[] dataBytes = Encoding.ASCII.GetBytes(data);
            stream.Write(dataBytes, 0, dataBytes.Length);
            Debug.Log($"Enviado para o servidor: {data}");

            // Após o login bem-sucedido, carregar a cena principal
            LoadMainMenuScene();
        }
        else
        {
            Debug.LogError("Não foi possível enviar os dados. Conexão não estabelecida.");
        }
    }

    // Carregar a cena principal (Main Menu)
    private void LoadMainMenuScene()
    {
        // Carregar a cena "MainMenu" após o login
        SceneManager.LoadScene("MainMenu");
    }

    // Método para desconectar do servidor
    private void OnApplicationQuit()
    {
        if (client != null)
        {
            stream.Close();
            client.Close();
            Debug.Log("Desconectado do servidor.");
        }
    }

    // Atualiza as informações do jogador no texto
    public void UpdatePlayerInfoText()
    {
        if (playerInfoText != null)
        {
            playerInfoText.text = $"Nome: {playerName} | Pontuação: {playerScore}";
        }
    }

    // Método para adicionar pontos
    public void AddPoints(int points)
    {
        playerScore += points;
        UpdateScoreInServer(playerScore);
    }

    // Método para subtrair pontos (não permitindo que a pontuação seja menor que 0)
    public void SubtractPoints(int points)
    {
        playerScore = Mathf.Max(0, playerScore - points);
        UpdateScoreInServer(playerScore);
    }

    // Envia a atualização de score para o servidor
    private void UpdateScoreInServer(int newScore)
    {
        if (client != null && stream != null)
        {
            string data = $"updateScore,{playerName},{newScore}";
            byte[] dataBytes = Encoding.ASCII.GetBytes(data);
            stream.Write(dataBytes, 0, dataBytes.Length);
            Debug.Log($"Enviado para o servidor: {data}");

            // Atualiza a interface após o envio
            UpdatePlayerInfoText();
        }
        else
        {
            Debug.LogError("Não foi possível enviar os dados. Conexão não estabelecida.");
        }
    }

    // Método para atualizar as informações do jogador quando a cena for carregada
    void OnLevelWasLoaded(int level)
    {
        // Verifique se estamos na MainMenu (pode ser pelo nome da cena)
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            // Atualize as informações do jogador no texto
            UpdatePlayerInfoText();
        }
    }*/
}
