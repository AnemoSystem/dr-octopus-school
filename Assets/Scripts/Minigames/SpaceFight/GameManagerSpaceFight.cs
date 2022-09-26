using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSpaceFight : MonoBehaviour
{
    public GameObject gameElements;
    public GameObject hud;
    public GameObject menu;

    void Start()
    {
        menu.SetActive(true);
        hud.SetActive(false);
        gameElements.SetActive(false);
    }

    public void StartGame() {
        menu.SetActive(false);
        hud.SetActive(true);
        gameElements.SetActive(true);     
    }
}