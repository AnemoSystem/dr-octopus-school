using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerController : MonoBehaviour
{
    public GameObject player;
    public GameObject spawner;
    public GameObject menu;
    public GameObject inGame;

    public void StartGame() {
        player.SetActive(true);
        spawner.SetActive(true);
        menu.SetActive(false);
        inGame.SetActive(true);
    }
}