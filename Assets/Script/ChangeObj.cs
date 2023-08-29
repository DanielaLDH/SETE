using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeObj : MonoBehaviour
{
    public GameObject inteiro;
    public GameObject quebrado;

    public int sceneToLoad; // Nome da cena a ser carregada
    public float interactRange;
    public int num;

    private bool playerHit;
    private bool hasInteracted; // Variável específica para este objeto

    // Start is called before the first frame update
    void Start()
    {
        
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;


        // Carrega o valor da variável específica para este objeto
        hasInteracted = PlayerPrefs.GetInt(gameObject.tag + "_HasInteracted", 0) == 1;

        if (currentSceneIndex == 0)
        {

            GameObject position = GameObject.FindGameObjectWithTag("Player");

            float posX = PlayerPrefs.GetFloat("PosX", 0f);
            float posY = PlayerPrefs.GetFloat("PosY", 0f);

            position.transform.position = new Vector2(posX, posY);
        }

        if (currentSceneIndex == 1)
        {
            int item = PlayerPrefs.GetInt("item");
            

            if (num != item && !hasInteracted)
            {
                Debug.Log(item + "  " + num);
                gameObject.SetActive(false);
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        ValidaInteract();



        if (Input.GetKeyDown(KeyCode.E) && playerHit && !hasInteracted)
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            if (currentSceneIndex == 0)
            {
                PlayerPrefs.SetInt("item", num);

                GameObject position = GameObject.FindGameObjectWithTag("Player");

                PlayerPrefs.SetFloat("PosX", position.transform.position.x);
                PlayerPrefs.SetFloat("PosY", position.transform.position.y);

                inteiro.SetActive(true);
                quebrado.SetActive(false);
            }
            else if (currentSceneIndex == 1)
            {
                inteiro.SetActive(true);
            }
            StartCoroutine(Recover());
            SceneTransition();

            // Marca a variável específica deste objeto como "interagida"
            hasInteracted = true;
            PlayerPrefs.SetInt(gameObject.tag + "_HasInteracted", 1);
        }
    }


    void ValidaInteract()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (hasInteracted && currentSceneIndex == 0)
        {
            inteiro.SetActive(true);
            quebrado.SetActive(false);
        }
        else if (hasInteracted && currentSceneIndex == 1)
        {
            inteiro.SetActive(true);
        }
    }

    private IEnumerator Recover()
    {
        yield return new WaitForSeconds(50);
    }

    void FixedUpdate()
    {
        Interact();
    }


    void Interact()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, interactRange);

        if (hit != null && hit.CompareTag("Player"))
        {
            playerHit = true;
        }
        else
        {
            playerHit = false;
        }
    }

    void SceneTransition()
    {
        
        SceneManager.LoadScene(sceneToLoad);
        PlayerPrefs.SetInt("HasInteracted", 1); // Definindo a variável como true após a mudança de cena

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }

}
