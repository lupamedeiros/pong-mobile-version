using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class MenuManager : MonoBehaviour
{
    public Text playerNameText; // Texto para mostrar o nome do jogador
    public GameObject waitingPanel; // Painel que exibe "Aguardando Oponentes"
    public GameObject menuPanel; // Painel do menu principal
    private MatchmakingManager matchmakingManager;

    void Start()
    {
        // Exibe o nome do jogador que foi definido no LoginManager
        playerNameText.text = PhotonNetwork.NickName;

        // Inicializa o MatchmakingManager
        matchmakingManager = FindObjectOfType<MatchmakingManager>();

        // Inicialmente, esconde o painel de espera e mostra o menu
        menuPanel.SetActive(true);
        waitingPanel.SetActive(false);
    }

    // Método chamado quando o jogador clica em "Jogar"
    public void OnPlayButtonClicked()
    {
        menuPanel.SetActive(false); // Esconde o painel do menu
        waitingPanel.SetActive(true); // Exibe o painel de espera

        // Inicia o matchmaking
        matchmakingManager.StartMatchmaking();
    }
}