using Photon.Pun;
using UnityEngine;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public GameManager gameManager; // Referência ao GameManager

    void Start()
    {
        // Conectar ao servidor Photon
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectado ao servidor Photon.");
        // Entrar em uma sala aleatória
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Falha ao tentar entrar em uma sala aleatória. Criando uma nova sala.");
        // Cria uma sala nova se nenhuma estiver disponível
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 2 });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Entrou na sala. Instanciando jogador...");
        if (gameManager != null)
        {
            // Chama o método de instânciação no GameManager
            gameManager.InstantiatePlayer();
            Debug.Log("Jogador encontrado.");
        }
        else
        {
            Debug.LogError("GameManager não está atribuído ao PhotonManager!");
        }
    }
}