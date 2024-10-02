using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    public GameObject enemyPrefab;   // O prefab da nave inimiga
    private float initialSpawnDelay = 2f;  // Tempo inicial entre os spawns
    private float minimumSpawnDelay = 0.2f; // Tempo mínimo entre os spawns
    public BoxCollider2D spawnArea;  // O BoxCollider2D que define a área de spawn
    private float timeToReduceDelay = 10f;  // Tempo para diminuir o spawnDelay

    private float spawnDelay;        // Tempo entre os spawns (variável mutável)
    private float elapsedTime = 0f;  // Tempo decorrido desde o início do jogo

    private void Start()
    {
        // Inicializa o spawnDelay com o valor inicial
        spawnDelay = initialSpawnDelay;

        // Inicia o processo de spawn repetidamente
        StartCoroutine(SpawnEnemies());
    }

    private void Update()
    {
        // Atualiza o tempo decorrido
        elapsedTime += Time.deltaTime;

        // A cada 30 segundos, diminui o tempo de spawnDelay
        if (elapsedTime >= timeToReduceDelay)
        {
            // Diminui o delay, respeitando o mínimo
            spawnDelay = Mathf.Max(spawnDelay - 0.2f, minimumSpawnDelay);

            // Reseta o tempo decorrido
            elapsedTime = 0f;

            // Log para depuração
            Debug.Log($"Novo tempo de spawn: {spawnDelay} segundos");
        }
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
