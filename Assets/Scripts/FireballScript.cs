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
            Debug.Log("Fireball hit the enemy!"); 
            
            // Destruir o inimigo atingido
            other.gameObject.GetComponent<ExplosionController>().StartExplosion();

            // Destruir a fireball após a colisão
            Destroy(gameObject);
        }
    }
}
