using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab; // Prefab do jogador a ser instanciado
    public GameObject waitingPanel; // Painel de espera
    public Text countdownText;      // Texto da contagem regressiva
    public GameObject ballPrefab;   // Prefab da bola
    public Text leftScoreText;      // Texto da pontuação do jogador esquerdo
    public Text rightScoreText;     // Texto da pontuação do jogador direito
    public int countdownTime = 3;   // Tempo da contagem regressiva

    private bool gameStarted = false;
    private GameObject ball;        // Referência para a bola instanciada
    private int leftScore = 0;
    private int rightScore = 0;

    void Start()
    {
        // Exibe o painel de espera até que dois jogadores entrem
        waitingPanel.SetActive(true);
        StartCoroutine(CheckPlayersReady());
        UpdateScoreUI();
    }

    public void AddScore(bool isLeft)
    {
        if (isLeft)
            leftScore++;
        else
            rightScore++;

        UpdateScoreUI();
        StartCoroutine(ResetBallAfterDelay());
    }

    private void UpdateScoreUI()
    {
        leftScoreText.text = leftScore.ToString();
        rightScoreText.text = rightScore.ToString();
    }

    IEnumerator ResetBallAfterDelay()
    {
        if (ball != null)
        {
            ball.GetComponent<PongBallController>().ResetBall();
        }

        countdownText.gameObject.SetActive(true);

        for (int i = countdownTime; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1);
        }

        countdownText.text = "GO!";
        yield return new WaitForSeconds(1);
        countdownText.gameObject.SetActive(false);

        if (ball != null)
        {
            ball.GetComponent<PongBallController>().LaunchBall();
        }
    }

    public void InstantiatePlayer()
    {
        if (playerPrefab != null)
        {
            Debug.Log("Instanciando jogador...");

            float spawnPosition = PhotonNetwork.PlayerList.Length == 1 ? 8f : -8f;
            Quaternion spawnRotation = PhotonNetwork.PlayerList.Length == 1
                ? Quaternion.Euler(0, 0, 90)
                : Quaternion.Euler(0, 0, -90);

            try
            {
                PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(spawnPosition, 0, 0), spawnRotation);
                Debug.Log("Jogador instanciado com sucesso!");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Erro ao instanciar o jogador: {e.Message}");
            }
        }
        else
        {
            Debug.LogError("Prefab do jogador não foi atribuído no GameManager!");
        }
    }

    IEnumerator CheckPlayersReady()
    {
        while (PhotonNetwork.PlayerList.Length < 2)
        {
            yield return null;
        }

        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        for (int i = countdownTime; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1);
        }

        countdownText.text = "GO!";
        yield return new WaitForSeconds(1);
        waitingPanel.SetActive(false);
        gameStarted = true;

        if (PhotonNetwork.IsMasterClient)
        {
            LaunchBall();
        }
    }

    void LaunchBall()
    {
        if (ballPrefab != null)
        {
            ball = PhotonNetwork.Instantiate(ballPrefab.name, Vector3.zero, Quaternion.identity);
            Debug.Log("Bola instanciada!");
        }
        else
        {
            Debug.LogError("Prefab da bola não foi atribuído no GameManager!");
        }
    }

    public bool IsGameStarted()
    {
        return gameStarted;
    }
}