using System.Collections;
using System.Collections.Generic;
using SmallShips;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour
{
    public float moveSpeed = 2;

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;

        // Movimentação do inimigo
        pos.x -= moveSpeed * Time.deltaTime;

        // Se o inimigo passar de certo ponto da tela, ele é destruído
        if (pos.x < -12)
        {
            Destroy(gameObject); // Destrói apenas a instância na cena, não o prefab original
        }

        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se a colisão foi com um objeto com tag "Enemy"
        if (other.CompareTag("Player"))
        {
            Debug.Log("enemy exploded player"); 
            
            // Destruir o inimigo atingido
            other.gameObject.GetComponent<ExplosionController>().StartExplosion();

            // Destruir a fireball após a colisão

            StartCoroutine(GameOverScreenCoroutine());
        }
    }

    IEnumerator GameOverScreenCoroutine()
    {
        Debug.Log("Pausa iniciada...");
        
        yield return new WaitForSeconds(1/2);
        
        gameObject.GetComponent<ExplosionController>().StartExplosion();

        SceneManager.LoadScene("JogoOver");
    }
}
