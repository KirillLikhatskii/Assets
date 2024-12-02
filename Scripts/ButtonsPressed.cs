using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsPressed : MonoBehaviour
{
    public GameObject menu, gunstats, HUD, pistol, SMG, player, enemy;
    public Transform[] spawnpoints;


    public void StartButtonPressed()
    {
        Time.timeScale = 1.0f;
        menu.SetActive(false);
        HUD.SetActive(true);
        gunstats.SetActive(pistol.active || SMG.active);
    }

    public void RestartButtonPressed()
    {
        Time.timeScale = 1.0f;
        menu.SetActive(false);
        SceneManager.LoadScene(0);
    }
}
