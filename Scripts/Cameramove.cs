using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameramove : MonoBehaviour
{

    private Transform player;

    private void LateUpdate()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 temp = transform.position;
        temp.x = player.position.x;
        temp.y = player.position.y;
        transform.position = temp;
    }
}
