using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    public GameObject enemyPrefab;  // O prefab da nave inimiga
    public float spawnDelay = 2f;   // Tempo entre os spawns
    public BoxCollider2D spawnArea; // O BoxCollider2D que define a área de spawn

    private void Start()
    {
        // Inicia o processo de spawn repetidamente
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        // Loop infinito para spawnar inimigos
        while (true)
        {
            // Aguardar pelo tempo definido em spawnDelay antes de spawnar outro inimigo
            yield return new WaitForSeconds(spawnDelay);

            // Gera uma posição aleatória dentro da área de spawn
            Vector3 spawnPosition = GetRandomSpawnPosition();

            // Instancia o prefab na posição aleatória e com a rotação original
            Instantiate(enemyPrefab, spawnPosition, enemyPrefab.transform.rotation);
        }
    }

    // Função para gerar uma posição aleatória dentro dos limites do BoxCollider2D
    private Vector3 GetRandomSpawnPosition()
    {
        // Pega as extremidades do BoxCollider2D (representando os limites da área de spawn)
        Bounds bounds = spawnArea.bounds;

        // Gera posições aleatórias dentro dos limites do BoxCollider2D
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);

        // Retorna a posição aleatória gerada
        return new Vector3(randomX, randomY, 0);
    }
}
