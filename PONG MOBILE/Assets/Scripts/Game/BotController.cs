using UnityEngine;

public class PongBotController : MonoBehaviour
{
    public bool isFacil = false;  // Ativa o bot fácil
    public bool isMedio = false;  // Ativa o bot médio
    public bool isDificil = false; // Ativa o bot difícil

    public float speed = 5f; // Velocidade do bot (fixa)
    public float delayFacil = 0.5f; // Delay para o bot fácil
    public float delayMedio = 0.3f; // Delay para o bot médio
    public float delayDificil = 0.1f; // Delay para o bot difícil

    private Transform ball; // Referência à bola
    private Rigidbody2D rb; // Referência ao Rigidbody2D do bot
    private float delayTime; // Tempo de delay
    private float timeSinceLastMove; // Controle de tempo para o delay

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ball = GameObject.FindGameObjectWithTag("Ball").transform; // Encontra a bola pela tag "Ball"
        timeSinceLastMove = 0f;
    }

    void FixedUpdate()
    {
        ball = GameObject.FindGameObjectWithTag("Ball").transform; // Encontra a bola pela tag "Ball"
        // Determina o delay com base na dificuldade
        if (isFacil)
        {
            delayTime = delayFacil;
        }
        else if (isMedio)
        {
            delayTime = delayMedio;
        }
        else if (isDificil)
        {
            delayTime = delayDificil;
        }

        // Conta o tempo desde o último movimento
        timeSinceLastMove += Time.fixedDeltaTime;

        // Só começa a mover o bot depois do delay
        if (timeSinceLastMove >= delayTime)
        {
            MoveBot();
        }
    }

    void MoveBot()
    {
        // Verifica a direção horizontal da bola
        Vector2 ballVelocity = ball.GetComponent<Rigidbody2D>().velocity;

        // Se a velocidade horizontal da bola for negativa (bola indo para o lado do bot)
        if (ballVelocity.x < 0)
        {
            // Calcula a distância entre o bot e a bola no eixo X
            float distanceToBallX = ball.position.x - transform.position.x;

            // Verifica se a bola está indo para o lado do bot
            if (distanceToBallX < 10f) // O bot começa a se mover quando a bola está perto do seu lado
            {
                // Calcula o tempo que a bola vai levar para chegar na altura do bot no eixo X
                float timeToReachBotX = Mathf.Abs(distanceToBallX) / Mathf.Abs(ballVelocity.x);

                // Calcula a posição futura da bola no eixo Y, baseado no tempo calculado
                float futureBallYPosition = ball.position.y + ballVelocity.y * timeToReachBotX;

                // O bot move-se diretamente para a posição antecipada no eixo Y
                Vector2 targetPosition = new Vector2(transform.position.x, futureBallYPosition);

                // Mover o bot para a posição futura no eixo Y da bola
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.fixedDeltaTime);
            }
        }
    }
}
