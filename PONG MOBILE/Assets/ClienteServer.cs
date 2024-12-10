using System;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class PongClient : MonoBehaviour
{
    private TcpClient client;
    private NetworkStream stream;
    private byte[] buffer = new byte[256];
    private string serverMessage; 

    void Start()
    {
        ConnectToServer();
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        if (stream != null && stream.DataAvailable)
        {
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            serverMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Debug.Log("Mensagem do servidor: " + serverMessage);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SendMessageToServer("JOGADA: SPACE");
        }
    }

    private void ConnectToServer()
    {
        try
        {
            client = new TcpClient("127.0.0.1", 13000);
            stream = client.GetStream();
            Debug.Log("Conectado ao servidor.");
        }
        catch (Exception e)
        {
            Debug.LogError("Erro ao conectar ao servidor: " + e.Message);
        }
    }

    private void SendMessageToServer(string message)
    {
        if (stream != null)
        {
            byte[] msg = Encoding.UTF8.GetBytes(message);
            stream.Write(msg, 0, msg.Length);
        }
    }

    private void OnApplicationQuit()
    {
        stream?.Close();
        client?.Close();
    }
}