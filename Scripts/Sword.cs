using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public float offset = 0;
    public GameObject bullet;
    public Transform shotpoint;

    private float timeBtwShots;
    public float startTimeBtwShots;

    private void Update()
    {
        if (timeBtwShots <= 0)
        {
            
            if (Input.GetMouseButton(0))
            {
                Instantiate(bullet, shotpoint.position, transform.rotation);
                timeBtwShots = startTimeBtwShots;
                
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
}
