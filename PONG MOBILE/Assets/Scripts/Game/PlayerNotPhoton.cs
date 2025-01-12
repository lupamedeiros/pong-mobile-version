using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNotPhoton : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    
    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * moveInput * moveSpeed * Time.deltaTime);
    }
}
