using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f; // Velocidade do jogador
    public string inputAxis = "Vertical"; // Define o eixo de entrada

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float move = Input.GetAxis(inputAxis);
        rb.velocity = new Vector2(0, move * speed);
    }
}