using UnityEngine;

public class PongManager : MonoBehaviour
{
    /*public GameObject pongBall; // Referência para a bola
    private PongBallController ballController; // Controlador da bola
    private PongScoreManager scoreManager; // Gerenciador de pontuação

    void Start()
    {
        ballController = pongBall.GetComponent<PongBallController>();
        scoreManager = FindObjectOfType<PongScoreManager>(); // Encontra o gerenciador de pontuação
    }

    void Update()
    {
        // Verifica se a bola passou para a esquerda ou para a direita
        if (pongBall.transform.position.x < -10) // Se a bola passar da linha da esquerda
        {
            scoreManager.ScorePoint("Right"); // Ponto para o jogador da direita
            ResetBall();
        }
        else if (pongBall.transform.position.x > 10) // Se a bola passar da linha da direita
        {
            scoreManager.ScorePoint("Left"); // Ponto para o jogador da esquerda
            ResetBall();
        }
    }

    // Método para reposicionar a bola no centro
    private void ResetBall()
    {
        // Reposiciona a bola no centro (0,0,0)
        pongBall.transform.position = Vector3.zero;

        // Reinicia a velocidade da bola
        ballController.ResetBall();
    }*/
}