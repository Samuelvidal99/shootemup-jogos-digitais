using System.Collections;
using System.Collections.Generic;
using SmallShips;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject parentShip;

     public Upgrade[] upgrades; // Array de armas
    private Upgrade currentUpgrade; // A arma atualmente equipada

    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;
    private bool canShoot = true; // Controla se o jogador pode disparar

    public float munition;
    public float maxMunition = 100f;

    public AmmunitionBarBehavior ammunitionBar;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        objectWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
        objectHeight = GetComponent<SpriteRenderer>().bounds.extents.y;

        if (upgrades.Length > 0)
        {
            currentUpgrade = upgrades[upgrades.Length - 1];
        }

        munition = 0f;
        ammunitionBar.SetAmmunition(munition);
        ammunitionBar.SetMaxAmmunition(maxMunition);
    }

    public void AddAmmo(int amount)
    {
        munition += amount;

        ammunitionBar.SetAmmunition(munition);
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

        // if (Input.GetMouseButtonDown(1))
        // {
        //     Destroy();
        // }
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

    void Destroy()
    {
        parentShip.GetComponent<ExplosionController>().StartExplosion();
    }
}
