using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject playerLeft;
    public GameObject playerRight;

    private int scoreLeft = 0;
    private int scoreRight = 0;

    void Start()
    {
        // Determina os papéis dos jogadores (quem é esquerda e quem é direita)
        if (PhotonNetwork.IsMasterClient)
        {
            playerRight.GetComponent<PlayerController>().isControlledLocally = true;
            playerRight.GetComponent<PlayerController>().AssignSide(false); // Player da direita
        }
        else
        {
            playerLeft.GetComponent<PlayerController>().isControlledLocally = true;
            playerLeft.GetComponent<PlayerController>().AssignSide(true); // Player da esquerda
        }
    }

    [PunRPC]
    public void ScorePoint(string side)
    {
        if (!PhotonNetwork.IsMasterClient) return; // Apenas o Master Client controla a pontuação

        if (side == "Left")
        {
            scoreLeft++;
        }
        else if (side == "Right")
        {
            scoreRight++;
        }

        // Sincronizar os pontos com todos os jogadores
        photonView.RPC("SyncScore", RpcTarget.All, scoreLeft, scoreRight);
    }

    [PunRPC]
    public void SyncScore(int left, int right)
    {
        scoreLeft = left;
        scoreRight = right;

        // Exibir os pontos na interface (se houver)
        Debug.Log($"Pontuação Atual - Esquerda: {scoreLeft} | Direita: {scoreRight}");
    }
}