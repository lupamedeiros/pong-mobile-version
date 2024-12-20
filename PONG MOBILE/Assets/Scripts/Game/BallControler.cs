using Photon.Pun;
using UnityEngine;

public class PongBallController : MonoBehaviourPun
{
    private Rigidbody2D rb;
    public float initialSpeed = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (photonView.IsMine)
        {
            LaunchBall();
        }
    }

    public void LaunchBall()
    {
        float xDirection = Random.Range(0, 2) == 0 ? -1f : 1f;
        float yDirection = Random.Range(-1f, 1f);

        Vector2 initialDirection = new Vector2(xDirection, yDirection).normalized;
        rb.velocity = initialDirection * initialSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!photonView.IsMine) return;

        if (other.CompareTag("Boundary"))
        {
            bool isLeft = transform.position.x > 0;
            FindObjectOfType<GameManager>().AddScore(isLeft);
        }
    }

    public void ResetBall()
    {
        rb.velocity = Vector2.zero;
        transform.position = Vector3.zero;
    }
}