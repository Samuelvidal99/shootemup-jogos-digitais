using System.Collections;
using System.Collections.Generic;
using SmallShips;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject parentShip;

    public Upgrade[] upgrades; // Array de upgrades
    private int currentUpgradeIndex = 0; // Índice do upgrade atualmente equipado
    private Upgrade currentUpgrade; // O upgrade atualmente equipado

    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;
    private bool canShoot = true; // Controla se o jogador pode disparar

    public float munition;
    public float maxMunition = 100f;

    public AmmunitionBarBehavior ammunitionBar;

    public ScreenGlowEffect screenGlowEffect;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        objectWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
        objectHeight = GetComponent<SpriteRenderer>().bounds.extents.y;

        // Começa com o primeiro upgrade da lista
        if (upgrades.Length > 0)
        {
            currentUpgrade = upgrades[currentUpgradeIndex];
        }

        munition = 0f;
        ammunitionBar.SetAmmunition(munition);
        ammunitionBar.SetMaxAmmunition(maxMunition);
    }

    public void AddAmmo(int amount)
    {
        munition += amount;

        // Atualiza a barra de munição
        ammunitionBar.SetAmmunition(munition);

        // Verifica se a munição atingiu ou ultrapassou o valor máximo
        if (munition >= maxMunition)
        {
            UpgradeToNext();
        }
    }

    private void UpgradeToNext()
    {
        if (currentUpgradeIndex < upgrades.Length - 1)
        {
            currentUpgradeIndex++;
            currentUpgrade = upgrades[currentUpgradeIndex];

            if (currentUpgradeIndex != upgrades.Length - 1) {
                munition = 0f; // Reseta a munição
                ammunitionBar.SetAmmunition(munition); // Atualiza a barra de munição
            }

            Debug.Log("Upgrade realizado: " + currentUpgradeIndex);
        }
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(moveVertical, moveHorizontal * -1, 0);

        transform.Translate(moveDirection * currentUpgrade.moveSpeed * Time.deltaTime);

        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);
        transform.position = clampedPosition;

        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            StartCoroutine(Shoot());
        }

        if (Input.GetMouseButtonDown(1) && munition >= 50)
        {
            Special();
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;  // Impede o disparo enquanto o delay está ativo

        if (currentUpgrade != null)
        {
            currentUpgrade.Shoot(bulletSpawnPoint);
        }

        yield return new WaitForSeconds(currentUpgrade.shootDelay); // Aguarda o tempo do delay

        canShoot = true; // Permite que o jogador dispare novamente
    }

    void Special()
    {
        Debug.Log("Special");

        screenGlowEffect.StartGlow();

        if (munition - 50f <= 0) {
            currentUpgradeIndex--;
            currentUpgrade = upgrades[currentUpgradeIndex];
        }
        munition -= 50f; // Reseta a munição
        ammunitionBar.SetAmmunition(munition); // Atualiza a barra de munição

        GameObject enemyGroupSpawner = GameObject.Find("EnemyType2Spawner");
        enemyGroupSpawner.GetComponent<EnemyGroupSpawner>().DestroyEnemyGroupSpecial();

        GameObject enemyGroupSpawner2 = GameObject.Find("EnemyType2Spawner 1");
        enemyGroupSpawner2.GetComponent<EnemyGroupSpawner>().DestroyEnemyGroupSpecial();

        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("EnemyType1");
        foreach (GameObject obj in objectsWithTag)
        {
            obj.GetComponent<ExplosionController>().StartExplosion();
        }
    }
}
