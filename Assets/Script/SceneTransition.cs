using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public int sceneToLoad; // Nome da cena a ser carregada

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Certifique-se de ter uma tag para o jogador
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
