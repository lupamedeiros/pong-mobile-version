using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class LoginManager : MonoBehaviourPunCallbacks
{
    public InputField playerNameInputField; // Campo para o nome do jogador

    // Método para o jogador fazer login
    public void OnLogin()
    {
        string playerName = playerNameInputField.text;
        if (!string.IsNullOrEmpty(playerName))
        {
            PhotonNetwork.NickName = playerName; // Define o nome do jogador
            PhotonNetwork.ConnectUsingSettings(); // Conecta ao Photon
        }
        else
        {
            Debug.Log("Nome do jogador não pode estar vazio");
        }
    }

    // Método chamado quando a conexão ao Photon é bem-sucedida
    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectado ao Photon");
        // Carrega a cena do menu após o login
        PhotonNetwork.LoadLevel("MainMenu");
    }
}