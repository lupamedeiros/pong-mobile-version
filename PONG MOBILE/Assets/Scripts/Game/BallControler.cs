using UnityEngine;
using Photon.Pun;

public class PongBallController : MonoBehaviourPun
{
    public float initialSpeed = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (photonView.IsMine) // Apenas o Master Client controla a lógica inicial da bola
        {
            ResetBall();
        }
    }

    public void ResetBall()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        transform.position = Vector3.zero;

        // Direção aleatória para a bola
        float xDirection = Random.Range(0, 2) == 0 ? -1 : 1;
        float yDirection = Random.Range(-1f, 1f);

        Vector2 direction = new Vector2(xDirection, yDirection).normalized;
        rb.velocity = direction * initialSpeed;

        // Sincronizar a posição e a velocidade da bola com todos os jogadores
        photonView.RPC("SyncBall", RpcTarget.All, rb.position, rb.velocity);
    }

    [PunRPC]
    public void SyncBall(Vector2 position, Vector2 velocity)
    {
        rb.position = position;
        rb.velocity = velocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Atualizar a posição e a velocidade da bola em todos os jogadores ao colidir com algo
        photonView.RPC("SyncBall", RpcTarget.All, rb.position, rb.velocity);
    }

    void OnBecameInvisible()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        if (transform.position.x < -10)
        {
            FindObjectOfType<GameManager>().photonView.RPC("ScorePoint", RpcTarget.All, "Right");
            ResetBall();
        }
        else if (transform.position.x > 10)
        {
            FindObjectOfType<GameManager>().photonView.RPC("ScorePoint", RpcTarget.All, "Left");
            ResetBall();
        }
    }
}