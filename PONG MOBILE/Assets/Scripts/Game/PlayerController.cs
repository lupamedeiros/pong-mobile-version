using UnityEngine;
using UnityEngine.EventSystems; // Necessário para usar OnPointerDown e OnPointerUp

public class PlayerController : MonoBehaviour
{
    public float speed = 10f; // Velocidade do jogador
    private Rigidbody2D rb;

    private float moveDirection = 0f; // Direção do movimento (-1, 0, 1)

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Define a velocidade com base na direção do movimento
        rb.velocity = new Vector2(0, moveDirection * speed);
    }

    // Funções para os botões
    public void MoveUp()
    {
        moveDirection = 1f; // Move para cima
    }

    public void MoveDown()
    {
        moveDirection = -1f; // Move para baixo
    }

    public void StopMoving()
    {
        moveDirection = 0f; // Para o movimento
    }
}