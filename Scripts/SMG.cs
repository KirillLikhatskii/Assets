using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMG : MonoBehaviour
{
    public float offset = 0;
    public GameObject bullet;
    public Transform shotpoint;
    public GameObject numOfAmmo;

    private float facing;
    private float timeBtwShots;
    public float startTimeBtwShots;

    private int ammo = 30;
    private float reload = 0;
    private float startReload = 5;
    private bool playerReload = false;

    private void Update()
    {
        numOfAmmo.GetComponent<TextMesh> ().text = ammo.ToString();
        facing = GameObject.FindGameObjectWithTag("Player").transform.localScale.x;
        if (facing < 0)
            offset = -180;
        else
            offset = 0;
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        if (rotZ * facing - offset < 50 && rotZ - offset > -50 || rotZ - offset < 50 && rotZ - offset > -50)
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
        if (timeBtwShots <= 0)
        {
            if ((reload <= 0 && ammo == 0) || (reload <= 0 && playerReload))
            {
                ammo = 30;
                playerReload = false;
            }
            if (Input.GetMouseButton(0) && ammo > 0 && !playerReload)
            {
                Instantiate(bullet, shotpoint.position, transform.rotation);
                timeBtwShots = startTimeBtwShots;
                ammo -= 1;
                if (ammo == 0)
                    reload = startReload;
            }
            if (ammo == 0 || playerReload)
            {
                reload -= Time.deltaTime;
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
        if (Input.GetKey("r"))
        {
            playerReload = true;
            reload = startReload;
        }
    }
}
