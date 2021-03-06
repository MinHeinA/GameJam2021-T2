using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject playerBullet;
    [SerializeField] private float bulletInterval = 5;
    [SerializeField] private WeaponbarBehaviour playerWeaponStatus;
    
    float weaponReady;
    Vector2 requiredVector;
    private float bulletCooldownTimer;
    private float weaponReadyMax;
    public float bulletForce = 20f;
    Vector3 mousePos;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        weaponReadyMax = bulletInterval;
        weaponReady = weaponReadyMax;
        playerWeaponStatus.SetWeaponStatus(weaponReady, weaponReadyMax);
    }

    // Update is called once per frame
    void Update()
    {
        var tempPos = Input.mousePosition;
        tempPos.z = 10;
        mousePos = cam.ScreenToWorldPoint(tempPos);

        requiredVector = (Vector2)mousePos - (Vector2)firepoint.position;

        if (bulletCooldownTimer > 0)
        {
            bulletCooldownTimer -= Time.deltaTime;
            weaponReady += Time.deltaTime;
            playerWeaponStatus.SetWeaponStatus(weaponReady, weaponReadyMax);
        }

        if (!GameManager.gameHasEnded)
        {
            if (Input.GetMouseButtonDown(0))
            {
                shoot();
            }
        }
    }

    void shoot()
    {
        float angle = Mathf.Atan2(requiredVector.y, requiredVector.x) * Mathf.Rad2Deg;

        if (bulletCooldownTimer > 0) return;

        weaponReady = 0;
        playerWeaponStatus.SetWeaponStatus(weaponReady, weaponReadyMax);

        bulletCooldownTimer = bulletInterval;

        GameObject bullet = Instantiate(playerBullet, firepoint.position, Quaternion.AngleAxis(angle, Vector3.forward));
        Rigidbody2D rigidBod = bullet.GetComponent<Rigidbody2D>();
        rigidBod.AddForce((Vector2)requiredVector * bulletForce, ForceMode2D.Impulse);
    }
}
