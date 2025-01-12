using System;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class ClienteServer : MonoBehaviour
{
    public static ClienteServer Instance; // Singleton Instance

    public string PlayerName { get; private set; }
    public int PlayerId { get; private set; }
    public int PlayerScore { get; private set; }

    private TcpClient client;
    private NetworkStream stream;
    private const string serverAddress = "127.0.0.1";
    private const int serverPort = 13000;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mantém entre cenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ConnectToServer()
    {
        try
        {
            client = new TcpClient(serverAddress, serverPort);
            stream = client.GetStream();
            Debug.Log("Conectado ao servidor.");
        }
        catch (Exception e)
        {
            Debug.LogError($"Erro ao conectar ao servidor: {e.Message}");
        }
    }

    public void SendPlayerName(string name)
    {
        if (stream == null)
        {
            Debug.LogError("Stream não inicializado. Verifique a conexão com o servidor.");
            return;
        }

        try
        {
            byte[] data = Encoding.ASCII.GetBytes(name);
            stream.Write(data, 0, data.Length);

            // Recebe resposta do servidor
            byte[] buffer = new byte[256];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);

            Debug.Log($"Resposta do servidor: {response}");

            // Processa resposta e atualiza os dados do jogador
            ProcessServerResponse(response);
        }
        catch (Exception e)
        {
            Debug.LogError($"Erro ao enviar dados ao servidor: {e.Message}");
        }
    }
    private void UpdateServerScore()
    {
        if (stream == null)
        {
            Debug.LogError("Stream não inicializado. Verifique a conexão com o servidor.");
            return;
        }

        try
        {
            // Envia uma mensagem ao servidor para atualizar o score
            string updateMessage = $"UPDATE_SCORE:ID:{PlayerId},Score:{PlayerScore}";
            byte[] data = Encoding.ASCII.GetBytes(updateMessage);
            stream.Write(data, 0, data.Length);

            // Recebe resposta do servidor
            byte[] buffer = new byte[256];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);

            Debug.Log($"Resposta do servidor sobre atualização do score: {response}");

            // Após atualizar o score, solicita o ranking atualizado
            SendRequest("GET_RANK");
        }
        catch (Exception e)
        {
            Debug.LogError($"Erro ao enviar atualização do score ao servidor: {e.Message}");
        }
    }

    private void ProcessServerResponse(string response)
    {
        // Espera-se uma resposta no formato: "ID:1,Name:John,Score:50"
        string[] parts = response.Split(',');
        foreach (var part in parts)
        {
            string[] keyValue = part.Split(':');
            if (keyValue[0] == "ID") PlayerId = int.Parse(keyValue[1]);
            else if (keyValue[0] == "Name") PlayerName = keyValue[1];
            else if (keyValue[0] == "Score") PlayerScore = int.Parse(keyValue[1]);
        }

        Debug.Log($"Jogador carregado - ID: {PlayerId}, Nome: {PlayerName}, Score: {PlayerScore}");
    }

    private void OnDestroy()
    {
        stream?.Close();
        client?.Close();
    }
    public void AddPoints(int points)
    {
        PlayerScore += points;
        UpdateServerScore();
    }
    public void SendRequest(string request)
    {
        if (stream == null)
        {
            Debug.LogError("Stream não inicializado. Verifique a conexão com o servidor.");
            return;
        }

        try
        {
            byte[] data = Encoding.ASCII.GetBytes(request);
            stream.Write(data, 0, data.Length);

            // Recebe resposta do servidor
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);

            Debug.Log($"Resposta do servidor: {response}");

            // Verifica se a solicitação foi para o ranking
            if (request == "GET_RANK")
            {
                // Atualiza o ranking visualmente
                FindObjectOfType<RankDisplay>()?.UpdateRankDisplay(response);
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Erro ao enviar solicitação ao servidor: {e.Message}");
        }
    }


    public void SubtractPoints(int points)
    {
        PlayerScore = Mathf.Max(0, PlayerScore - points); // Garante que o score não fique negativo
        UpdateServerScore();
    }
}
