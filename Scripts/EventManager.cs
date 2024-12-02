using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public GameObject menu, HUD, gunstats, pause, enemy, wawe, startbutton, restartbutton, gameover;
    public Transform[] enemyspawnpoints; 

    private int wave;
    private bool spawnStartWave = false;
    private void Start()
    {
        Time.timeScale = 0f;
    }


    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) 
        {
            restartbutton.SetActive(false);
            gameover.SetActive(false);
            pause.SetActive(true);
            HUD.SetActive(false);
            gunstats.SetActive(false);
            Time.timeScale = 0f;
            menu.SetActive(true);
            startbutton.SetActive(true);
        }
        if (GameObject.FindGameObjectsWithTag("Enemy").Length ==0)
        {
            wave += 1;
            spawnStartWave = true;
            wawe.GetComponent<TextMesh>().text = wave.ToString();
        }
        if (GameObject.FindGameObjectsWithTag("Player").Length == 0)
        {
            pause.SetActive(false);
            gameover.SetActive(true);
            startbutton.SetActive(false);
            HUD.SetActive(false);
            gunstats.SetActive(false);
            Time.timeScale = 0f;
            menu.SetActive(true);
            restartbutton.SetActive(true);
        }
        if(spawnStartWave)
        {
            for(int i = 0; i < wave*3; i++) 
            {
                Instantiate(enemy, enemyspawnpoints[i % 4]);
            }
            spawnStartWave=false;
        }

    }
}
