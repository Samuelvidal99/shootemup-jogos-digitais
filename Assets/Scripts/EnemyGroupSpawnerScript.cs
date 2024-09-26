using System.Collections;
using UnityEngine;

public class EnemyGroupSpawner : MonoBehaviour
{
    public GameObject enemyGroupPrefab; // O prefab que contém todos os inimigos do tipo 2
    private float respawnDelay = 10f; // Tempo de espera para respawn
    private float minimumRespawnDelay = 1f; // Tempo mínimo para respawn
    private float gameTime = 0f; // Contador de tempo de jogo

    private Vector3 initialPosition; // Posição inicial do grupo
    private GameObject currentEnemyGroup; // Referência ao grupo de inimigos atual
    private bool isRespawning = false; // Controle para evitar respawns duplicados

    void Start()
    {
        // Armazena a posição inicial do grupo
        initialPosition = transform.position;

        // Instancia o grupo pela primeira vez
        SpawnEnemyGroup();
    }

    void Update()
    {
        // Atualiza o tempo de jogo
        gameTime += Time.deltaTime;

        // Verifica se já se passaram 30 segundos
        if (gameTime >= 30f)
        {
            // Diminui o tempo de respawn
            respawnDelay = Mathf.Max(respawnDelay - 2f, minimumRespawnDelay);
            gameTime = 0f; // Reseta o contador
            Debug.Log($"Tempo de respawn atualizado: {respawnDelay} segundos"); // Log para verificar o novo tempo de respawn
        }
    }

    public void DestroyEnemyGroup()
    {
        if (currentEnemyGroup != null && NoEnemiesLeft())
        {
            Debug.Log("Destruindo grupo de inimigos."); // Log para verificar a destruição
            Destroy(currentEnemyGroup);
            currentEnemyGroup = null; // Limpa a referência após a destruição
            StartRespawn(); // Inicia o processo de respawn
        }
    }

    private void StartRespawn()
    {
        if (!isRespawning)
        {
            isRespawning = true; // Marca que estamos em respawn
            StartCoroutine(RespawnEnemyGroup());
        }
    }

    IEnumerator RespawnEnemyGroup()
    {
        Debug.Log("Aguardando respawn do grupo de inimigos..."); // Log para verificar o início do respawn
        yield return new WaitForSeconds(respawnDelay);

        // Respawn do grupo na posição inicial
        SpawnEnemyGroup();
        isRespawning = false; // Permite novos respawns
        Debug.Log("Grupo de inimigos respawnado."); // Log para verificar a conclusão do respawn
    }

    private void SpawnEnemyGroup()
    {
        // Instancia o prefab do grupo de inimigos na posição inicial
        currentEnemyGroup = Instantiate(enemyGroupPrefab, initialPosition, Quaternion.identity);
        Debug.Log("Grupo de inimigos instanciado."); // Log para verificar a instância do grupo
    }

    private bool NoEnemiesLeft()
    {
        // Verifica se ainda existem inimigos ativos no grupo
        Debug.Log(currentEnemyGroup.transform.childCount);
        return currentEnemyGroup.transform.childCount == 1;
    }
}
