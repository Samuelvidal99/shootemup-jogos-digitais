using UnityEngine;

[System.Serializable] // Isso permite que a classe apareça no Inspector do Unity
public class Upgrade
{
    // Campos da classe
    public GameObject bulletPrefab; // O prefab da bala
    public float bulletSpeed; // A velocidade da bala

    public float shootDelay;

    public float moveSpeed;

    // Construtor da classe (opcional)
    public Upgrade(GameObject bulletPrefab, float bulletSpeed)
    {
        this.bulletPrefab = bulletPrefab;
        this.bulletSpeed = bulletSpeed;
    }

    // Método para disparar a arma (opcional)
    public void Shoot(Transform bulletSpawnPoint)
    {
        GameObject bullet = Object.Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = bulletSpeed * (-bulletSpawnPoint.up);
    }
}
