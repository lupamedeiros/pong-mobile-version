using Photon.Pun;
using UnityEngine;

public class PlayerMovement : MonoBehaviourPun
{
    public float moveSpeed = 5f;

    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        // Verifica se o jogador tem a propriedade IsMine e se o jogo já começou
        if (photonView.IsMine && gameManager != null && gameManager.IsGameStarted())
        {
            float moveInputVertical = 0f;

            // Detecta movimento vertical com as teclas W/S
            if (Input.GetKey(KeyCode.W)) moveInputVertical = 1f; // Cima
            if (Input.GetKey(KeyCode.S)) moveInputVertical = -1f; // Baixo

            // Movimento vertical (cima/baixo)
            transform.Translate(Vector3.up * moveInputVertical * moveSpeed * Time.deltaTime);
        }
    }
}