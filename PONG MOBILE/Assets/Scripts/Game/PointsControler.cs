using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PointsControler : MonoBehaviour
{
    public Transform ball; // Referência ao transform da bola
    public Text player1ScoreText; // UI Text para o placar do jogador 1
    public Text player2ScoreText; // UI Text para o placar do jogador 2
    public Text countdownText; // UI Text para o contador regressivo
    public Text winnerText; // UI Text para mostrar o vencedor
    public GameObject endGamePanel; // Painel de fim de jogo com opções

    public float resetDelay = 1f; // Tempo antes de iniciar a contagem regressiva
    private int player1Score = 0; // Pontuação do jogador 1
    private int player2Score = 0; // Pontuação do jogador 2
    private Vector2 initialBallPosition; // Posição inicial da bola

    private Rigidbody2D ballRigidbody;

    private bool isGameOver = false; // Flag para verificar se o jogo acabou

    private void Start()
    {
        // Salva a posição inicial da bola e obtém o Rigidbody2D
        initialBallPosition = ball.position;
        ballRigidbody = ball.GetComponent<Rigidbody2D>();

        // Certifique-se de que o painel de fim de jogo está desativado no início
        endGamePanel.SetActive(false);

        ResetBall();
    }

    private void Update()
    {
        // Só executa se o jogo não acabou
        if (isGameOver)
            return;

        // Checa se a bola passou dos limites e adiciona pontos
        if (ball.position.x > 10)
        {
            player1Score++;
            UpdateScore();
            if (CheckWinCondition()) return;
            StartCoroutine(ResetBallWithCountdown());
        }
        else if (ball.position.x < -10)
        {
            player2Score++;
            UpdateScore();
            if (CheckWinCondition()) return;
            StartCoroutine(ResetBallWithCountdown());
        }
    }

    private void UpdateScore()
    {
        // Atualiza os textos de pontuação
        player1ScoreText.text = player1Score.ToString();
        player2ScoreText.text = player2Score.ToString();
    }

    private IEnumerator ResetBallWithCountdown()
    {
        // Para o movimento da bola e a coloca no centro
        ballRigidbody.velocity = Vector2.zero;
        ball.position = initialBallPosition;

        // Mostra a contagem regressiva
        countdownText.gameObject.SetActive(true);
        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        countdownText.gameObject.SetActive(false);

        // Lança a bola em uma direção aleatória
        LaunchBall();
    }

    private void ResetBall()
    {
        // Reseta a posição da bola e inicia a contagem regressiva
        ball.position = initialBallPosition;
        StartCoroutine(ResetBallWithCountdown());
    }

    private void LaunchBall()
    {
        // Define uma direção aleatória e lança a bola
        float xDirection = Random.Range(0, 2) == 0 ? 1 : -1;
        float yDirection = Random.Range(-1f, 1f);
        Vector2 launchDirection = new Vector2(xDirection, yDirection).normalized;
        ballRigidbody.velocity = launchDirection * 10f; // Ajuste a velocidade conforme necessário
    }

    private bool CheckWinCondition()
    {
        if (player1Score >= 10)
        {
            EndGame("Player da Esquerda Ganhou!");
            return true;
        }
        else if (player2Score >= 10)
        {
            EndGame("Player da Direita Ganhou!");
            return true;
        }
        return false;
    }

    private void EndGame(string winnerMessage)
    {
        // Evita que o jogo faça mais verificações após o término
        isGameOver = true;

        // Mostra o vencedor e ativa o painel de fim de jogo
        winnerText.text = winnerMessage;

        // Espera 2 segundos antes de ativar o painel
        Invoke(nameof(ShowEndGamePanel), 2f);
    }

    private void ShowEndGamePanel()
    {
        endGamePanel.SetActive(true);
    }

    // Botão para reiniciar a partida
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Botão para voltar ao menu
    public void ReturnToMenu()
    {
        // Substitua "MainMenu" pelo nome da sua cena de menu principal
        SceneManager.LoadScene("MainMenu");
    }
}
