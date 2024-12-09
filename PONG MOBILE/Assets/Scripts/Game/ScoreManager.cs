using UnityEngine;
using UnityEngine.UI; // Para atualizar o texto da pontuação
using System.Collections; // Para usar coroutines

public class PongScoreManager : MonoBehaviour
{
    public Text leftPlayerScoreText; // Referência para o texto da pontuação do jogador da esquerda
    public Text rightPlayerScoreText; // Referência para o texto da pontuação do jogador da direita
    public GameObject pongBallPrefab; // Prefab da bola que será instanciado no centro
    public Transform spawnPoint; // Ponto de spawn para a bola (geralmente o centro do campo)
    public Text countdownText; // Referência para o texto da contagem regressiva

    private int leftPlayerScore = 0;
    private int rightPlayerScore = 0;
    private bool isCountingDown = false; // Flag para controlar a contagem regressiva

    void Start()
    {
        // Inicia a bola no centro do campo
        SpawnNewBall();
    }

    // Método chamado para atualizar a pontuação
    public void ScorePoint(string side)
    {
        if (side == "Left")
        {
            rightPlayerScore++; // O ponto foi marcado pelo jogador da direita
        }
        else if (side == "Right")
        {
            leftPlayerScore++; // O ponto foi marcado pelo jogador da esquerda
        }

        // Atualiza a UI com a nova pontuação
        UpdateScoreUI();

        // Inicia a contagem regressiva e a reinicialização da bola
        if (!isCountingDown)
        {
            StartCoroutine(StartCountdown());
        }
    }

    // Método para atualizar os textos de pontuação
    private void UpdateScoreUI()
    {
        leftPlayerScoreText.text = leftPlayerScore.ToString();
        rightPlayerScoreText.text =  rightPlayerScore.ToString();
    }

    // Método para instanciar uma nova bola no centro
    private void SpawnNewBall()
    {
        // Destrói a bola atual (se houver) e cria uma nova no centro
        foreach (var ball in GameObject.FindGameObjectsWithTag("Ball"))
        {
            Destroy(ball);
        }

        // Instancia uma nova bola no centro
        Instantiate(pongBallPrefab, spawnPoint.position, Quaternion.identity);
    }

    // Coroutine para a contagem regressiva
    private IEnumerator StartCountdown()
    {
        isCountingDown = true;

        // Mostra o texto da contagem regressiva e inicia a contagem
        countdownText.gameObject.SetActive(true); // Ativa o texto

        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString(); // Atualiza o número no texto
            yield return new WaitForSeconds(1f); // Espera 1 segundo antes de mostrar o próximo número
        }

        countdownText.gameObject.SetActive(false); // Desativa o texto da contagem regressiva
        SpawnNewBall(); // Reinicia a bola no centro após a contagem
        isCountingDown = false;
    }
}
