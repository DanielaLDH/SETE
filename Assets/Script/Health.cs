using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int heartCount;

    public Image[] hearts;
    public Sprite heart;
    public Sprite noHeart;



    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = heart;  // Definir o sprite para o coração
            }
            else
            {
                hearts[i].color = new Color(1f, 1f, 1f, 0f); // Tornar a imagem completamente transparente

            }

            if (i < heartCount)
            {
                hearts[i].enabled = true;  // Tornar o sprite visível
            }
            else
            {
                hearts[i].enabled = false;  // Tornar o sprite invisível
            }
        }
    }
}
