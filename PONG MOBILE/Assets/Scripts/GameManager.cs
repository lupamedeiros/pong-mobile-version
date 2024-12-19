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
    public int countdownTime = 3;   // Tempo da contagem regressiva

    private bool gameStarted = false;
    private GameObject ball;        // Referência para a bola instanciada

    void Start()
    {
        // Exibe o painel de espera até que dois jogadores entrem
        waitingPanel.SetActive(true);
        StartCoroutine(CheckPlayersReady());
    }

    // Método para instanciar o jogador
    public void InstantiatePlayer()
    {
        if (playerPrefab != null)
        {
            Debug.Log("Instanciando jogador...");

            // Define a posição e rotação do jogador com base no número de jogadores
            float spawnPosition = PhotonNetwork.PlayerList.Length == 1 ? 8f : -8f;
            Quaternion spawnRotation = PhotonNetwork.PlayerList.Length == 1
                ? Quaternion.Euler(0, 0, 90) // Primeiro jogador: Rotação -90°
                : Quaternion.Euler(0, 0, -90); // Segundo jogador: Rotação 90°

            // Instancia o jogador na posição e rotação corretas
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

    // Corrotina para verificar se os jogadores entraram na sala
    IEnumerator CheckPlayersReady()
    {
        // Aguarda até que dois jogadores entrem na sala
        while (PhotonNetwork.PlayerList.Length < 2)
        {
            yield return null; // Aguarda o próximo frame
        }

        // Quando ambos os jogadores entrarem, começa a contagem regressiva
        StartCoroutine(StartCountdown());
    }

    // Corrotina da contagem regressiva
    IEnumerator StartCountdown()
    {
        int count = countdownTime;

        // Exibe a contagem regressiva
        while (count > 0)
        {
            countdownText.text = count.ToString();
            yield return new WaitForSeconds(1);
            count--;
        }

        // Quando a contagem terminar, exibe "GO!" e remove o painel
        countdownText.text = "GO!";
        yield return new WaitForSeconds(1);
        waitingPanel.SetActive(false); // Remove o painel de espera
        gameStarted = true;            // Permite a movimentação

        // Agora instanciamos e lançamos a bola
        if (PhotonNetwork.IsMasterClient)  // Verifica se o jogador atual é o Master Client
        {
            LaunchBall();
        }
    }

    // Método para instanciar e lançar a bola
// Método para instanciar e lançar a bola
    void LaunchBall()
    {
        if (ballPrefab != null)
        {
            // Instancia a bola no centro da cena
            ball = PhotonNetwork.Instantiate(ballPrefab.name, Vector3.zero, Quaternion.identity);
            Debug.Log("Bola instanciada!");

            // Adiciona um impulso à bola em uma direção aleatória
            Rigidbody rb = ball.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Direção aleatória
                float xDirection = Random.Range(0, 2) == 0 ? -1f : 1f; // Direção aleatória no eixo X (esquerda ou direita)
                float yDirection = Random.Range(-1f, 1f); // Direção aleatória no eixo Y (para cima ou para baixo)
            
                Vector3 randomDirection = new Vector3(xDirection, yDirection, 0).normalized; // Direção normalizada
                rb.AddForce(randomDirection * 10f, ForceMode.Impulse); // Aplica um impulso na direção aleatória com força de 10
            }
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
