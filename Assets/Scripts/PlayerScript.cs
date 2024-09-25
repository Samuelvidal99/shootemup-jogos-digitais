using System.Collections;
using System.Collections.Generic;
using SmallShips;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform bulletSpawnPoint;
    public GameObject parentShip;

    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        objectWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
        objectHeight = GetComponent<SpriteRenderer>().bounds.extents.y;
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(moveVertical, moveHorizontal * -1, 0);

        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);
        transform.position = clampedPosition;

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (Input.GetMouseButtonDown(1))
        {
            Destroy();
        }
    }

    void Shoot()
    {
        parentShip.GetComponent<BaseBulletStarter>().MakeOneShot();
    }

    void Destroy()
    {
        parentShip.GetComponent<ExplosionController>().StartExplosion();
    }
}
