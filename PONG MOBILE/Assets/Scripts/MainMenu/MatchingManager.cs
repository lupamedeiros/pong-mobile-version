using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MatchmakingManager : MonoBehaviourPunCallbacks
{
    // Chamado quando o jogador clica no botão "Jogar" no menu
    public void StartMatchmaking()
    {
        // Tenta juntar-se a uma sala aleatória
        PhotonNetwork.JoinRandomRoom();
    }

    // Chamado quando o jogador entra com sucesso em uma sala
    public override void OnJoinedRoom()
    {
        Debug.Log("Jogador entrou na sala: " + PhotonNetwork.CurrentRoom.Name);
        
        // Verifica se há 2 jogadores na sala e começa a partida
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            PhotonNetwork.LoadLevel("CompetitiveScene");
        }
    }

    // Chamado quando o jogador falha ao entrar em uma sala aleatória
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Falha ao entrar em uma sala aleatória. Criando uma nova sala...");
        
        // Cria uma nova sala para 2 jogadores
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(null, roomOptions);
    }

    // Chamado quando um jogador entra na sala
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        // Quando houver 2 jogadores, carrega a cena do jogo
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            PhotonNetwork.LoadLevel("CompetitiveScene");
        }
    }
}