using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviourPun
{
    public float speed = 10f;
    private Rigidbody2D rb;

    public bool isControlledLocally = false; // Este jogador é controlado localmente?
    private bool isPlayerLeft = false; // Determina se é o jogador da esquerda

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Configura o lado do jogador
    public void AssignSide(bool isLeft)
    {
        isPlayerLeft = isLeft;
    }

    // Método chamado para mover para cima
    public void MoveUp()
    {
        if (isControlledLocally)
        {
            rb.velocity = new Vector2(0, speed);
            photonView.RPC("SyncPlayerPosition", RpcTarget.Others, rb.position); // Sincroniza a posição
        }
    }

    // Método chamado para mover para baixo
    public void MoveDown()
    {
        if (isControlledLocally)
        {
            rb.velocity = new Vector2(0, -speed);
            photonView.RPC("SyncPlayerPosition", RpcTarget.Others, rb.position); // Sincroniza a posição
        }
    }

    // Método chamado para parar o movimento
    public void StopMoving()
    {
        if (isControlledLocally)
        {
            rb.velocity = Vector2.zero;
            photonView.RPC("SyncPlayerPosition", RpcTarget.Others, rb.position); // Sincroniza a posição
        }
    }

    // RPC para sincronizar a posição do jogador
    [PunRPC]
    public void SyncPlayerPosition(Vector2 position)
    {
        if (!isControlledLocally)
        {
            rb.position = position;
        }
    }
}