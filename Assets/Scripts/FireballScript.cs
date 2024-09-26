using System.Collections;
using System.Collections.Generic;
using SmallShips;
using UnityEngine;

public class FireballScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se a colisão foi com um objeto com tag "Enemy"
        if (other.CompareTag("EnemyType1"))
        {
            // Destruir o inimigo atingido
            other.gameObject.GetComponent<ExplosionController>().StartExplosion();

            // Destruir a fireball após a colisão
            Destroy(gameObject);
        }

        if (other.CompareTag("EnemyType2"))
        {
            // Destruir o inimigo atingido
            // other.gameObject.GetComponent<ExplosionController>().StartExplosion();
            Destroy(other.gameObject); // Destroi a nave

            // Verifica se o grupo de inimigos deve ser destruído
            GameObject enemyGroupSpawner = GameObject.Find("EnemyType2Spawner");
            enemyGroupSpawner.GetComponent<EnemyGroupSpawner>().DestroyEnemyGroup();

            Destroy(gameObject); // Destroi a fireball
        }
    }
}
