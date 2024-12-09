using UnityEngine;

public class PongBallController : MonoBehaviour
{
    public float initialSpeed = 5f; // Velocidade inicial
    public float speedIncrease = 0.5f; // Aumento de velocidade ao colidir com o jogador
    public float maxSpeed = 15f; // Velocidade máxima permitida
    public float VelocidadeConstante = 7f; // Velocidade constante desejada
    public float horizontalImpulseFactor = 1.5f; // Impulso adicional na direção horizontal após colisão
    public float randomImpulseFactor = 3f; // Fator de impulso aleatório em direção oposta

    private Rigidbody2D rb;
    private PongScoreManager scoreManager; // Referência ao gerenciador de pontuação

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scoreManager = FindObjectOfType<PongScoreManager>(); // Encontra o objeto PongScoreManager
        LaunchBall();
    }

    void LaunchBall()
    {
        // Lança a bola em uma direção aleatória
        float xDirection = Random.Range(0, 2) == 0 ? -1 : 1;
        float yDirection = Random.Range(-1f, 1f);
        Vector2 initialDirection = new Vector2(xDirection, yDirection).normalized;
        rb.velocity = initialDirection * initialSpeed;
    }

    void FixedUpdate()
    {
        // Verifica se a velocidade da bola está diferente da velocidade constante e ajusta
        if (rb.velocity.magnitude != VelocidadeConstante)
        {
            rb.velocity = rb.velocity.normalized * VelocidadeConstante;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Ajusta a velocidade ao colidir com o jogador
            float newSpeed = rb.velocity.magnitude + speedIncrease;
            newSpeed = Mathf.Min(newSpeed, maxSpeed); // Limita a velocidade máxima
            rb.velocity = rb.velocity.normalized * newSpeed;

            // Aplica um impulso maior na direção horizontal para evitar subidas e descidas infinitas
            Vector2 newVelocity = rb.velocity;
            newVelocity.x *= horizontalImpulseFactor; // Impulso maior na direção X
            rb.velocity = newVelocity;

            // Aumenta a VelocidadeConstante em 0.2 até o máximo de 15
            VelocidadeConstante = Mathf.Min(VelocidadeConstante + 0.2f, maxSpeed);

            // Aplica um impulso aleatório em direção oposta
            Vector2 randomImpulse = new Vector2(Random.Range(-randomImpulseFactor, randomImpulseFactor), Random.Range(-randomImpulseFactor, randomImpulseFactor));
            rb.velocity = rb.velocity + randomImpulse;
        }
    }

    void OnBecameInvisible()
    {
        // Verifica se a bola saiu pela esquerda ou direita e chama o método de pontuação
        if (transform.position.x < -10) // Ajuste esse valor conforme o limite do seu campo
        {
            scoreManager.ScorePoint("Right"); // Ponto para o jogador da direita
        }
        else if (transform.position.x > 10) // Ajuste esse valor conforme o limite do seu campo
        {
            scoreManager.ScorePoint("Left"); // Ponto para o jogador da esquerda
        }
    }
}
