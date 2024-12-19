using Photon.Pun;
using UnityEngine;

public class PongBallController : MonoBehaviourPun
{
    private Rigidbody2D rb;  // Referência ao Rigidbody2D para controlar o movimento
    public float initialSpeed = 10f; // Velocidade inicial da bola
    private Vector2 initialDirection; // Direção inicial da bola

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtemos o Rigidbody2D da bola

        if (photonView.IsMine) // Só o jogador local (o que controla a bola) aplica a lógica de movimento
        {
            // Direção aleatória
            float xDirection = Random.Range(0, 2) == 0 ? -1f : 1f; // Direção aleatória no eixo X
            float yDirection = Random.Range(-1f, 1f); // Direção aleatória no eixo Y

            initialDirection = new Vector2(xDirection, yDirection).normalized;

            // Aplica o movimento inicial
            rb.velocity = initialDirection * initialSpeed;
        }
    }

    void Update()
    {
        // Aqui, qualquer outra lógica de movimento ou colisão pode ser tratada
    }

    // Método para resetar a bola (chamado pelo GameManager quando necessário)
    public void ResetBall()
    {
        // Reseta a posição e a velocidade da bola
        rb.velocity = Vector2.zero;
        transform.position = Vector3.zero;

        // Aplica uma nova direção aleatória após reset
        float xDirection = Random.Range(0, 2) == 0 ? -1f : 1f;
        float yDirection = Random.Range(-1f, 1f);
        Vector2 newDirection = new Vector2(xDirection, yDirection).normalized;
        
        rb.velocity = newDirection * initialSpeed;
    }
}