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
            float moveInput = Input.GetAxisRaw("Horizontal");
            transform.Translate(Vector3.right * moveInput * moveSpeed * Time.deltaTime);
        }
    }
}