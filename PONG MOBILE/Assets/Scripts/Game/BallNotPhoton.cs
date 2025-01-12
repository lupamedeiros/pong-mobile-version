using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallNotPhoton : MonoBehaviour
{
    private Rigidbody2D rb;
    public float initialSpeed = 10f;
    public float speedIncrement = 1f; // Quanto a velocidade aumenta a cada colisão

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se a bola colidiu com um objeto com a tag "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            // Aumenta a velocidade atual da bola
            rb.velocity = rb.velocity.normalized * (rb.velocity.magnitude + speedIncrement);
        }
    }
}

