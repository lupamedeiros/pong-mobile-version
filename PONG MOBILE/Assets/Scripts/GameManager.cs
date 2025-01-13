using System.Collections;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab; 
    public GameObject waitingPanel;
    public Text countdownText;
    public GameObject ballPrefab; 
    public Text leftScoreText; 
    public Text rightScoreText;
    public int countdownTime = 3; 

    private bool gameStarted = false;
    private GameObject ball;
    private int leftScore = 0;
    private int rightScore = 0;

    public Text winnerText;  // Texto que exibirá quem ganhou

    void Start()
    {
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

        // Verifica se alguém atingiu 10 pontos
        if (leftScore >= 1)
        {
            Winner(true); // Jogador esquerdo ganha
        }
        else if (rightScore >= 1)
        {
            Winner(false); // Jogador direito ganha
        }
        else
        {
            StartCoroutine(ResetBallAfterDelay());
        }
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

    private void Winner(bool isLeft)
    {
        if (isLeft)
        {
            // Se o jogador da esquerda ganhou
            if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
            {
                // Se for o jogador 1 (isMine do jogador esquerdo)
                ClienteServer.Instance.AddPoints(10);  // Jogador esquerdo ganha
                ClienteServer.Instance.SubtractPoints(10); // Jogador direito perde
            }
            else
            {
                // Se for o jogador 2 (isMine do jogador direito)
                ClienteServer.Instance.AddPoints(10);  // Jogador esquerdo ganha
                ClienteServer.Instance.SubtractPoints(10); // Jogador direito perde
            }
            winnerText.text = "Jogador Esquerdo Venceu!";
        }
        else
        {
            // Se o jogador da direita ganhou
            if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
            {
                // Se for o jogador 2 (isMine do jogador direito)
                ClienteServer.Instance.AddPoints(10);  // Jogador direito ganha
                ClienteServer.Instance.SubtractPoints(10); // Jogador esquerdo perde
            }
            else
            {
                // Se for o jogador 1 (isMine do jogador esquerdo)
                ClienteServer.Instance.AddPoints(10);  // Jogador direito ganha
                ClienteServer.Instance.SubtractPoints(10); // Jogador esquerdo perde
            }
            winnerText.text = "Jogador Direito Venceu!";
        }

        StartCoroutine(LoadMainMenuAfterDelay());
    }

    IEnumerator LoadMainMenuAfterDelay()
    {
        yield return new WaitForSeconds(2f); // Espera 2 segundos antes de carregar o menu
        SceneManager.LoadScene("MainMenu"); // Nome da cena do menu inicial
    }

    public void InstantiatePlayer()
    {
        if (playerPrefab != null)
        {
            Debug.Log("Instanciando jogador...");

            float spawnPosition = PhotonNetwork.PlayerList.Length == 1 ? 8f : -8f;
            Quaternion spawnRotation = PhotonNetwork.PlayerList.Length == 1
                ? Quaternion.Euler(0, 180, 0)
                : Quaternion.Euler(0, 0, 0);

            try
            {
                GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(spawnPosition, 0, 0), spawnRotation);
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
