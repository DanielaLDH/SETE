using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertGravity : MonoBehaviour
{
    private bool isInverted = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Certifique-se de ter uma tag para o jogador
        {
            isInverted = !isInverted;
            Physics2D.gravity = isInverted ? new Vector2(0f, 9.81f) : new Vector2(0f, -9.81f);
        }
    }
}
