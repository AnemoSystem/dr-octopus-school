using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject Wind;
    public GameObject MenuPlayer;

    void Start() {
        CloseWind();
        CloseMenuPlayer();
    }

    public void OpenWind() {
        Wind.SetActive(true);
    }

    public void CloseWind () {
        Wind.SetActive(false);
    }

    public void OpenMenuPlayer () {
        MenuPlayer.SetActive(true);
        foreach(Transform child in MenuPlayer.transform) {
            child.gameObject.SetActive(true);
        }
    }

    public void CloseMenuPlayer () {
        MenuPlayer.SetActive(false);
        foreach(Transform child in MenuPlayer.transform) {
            child.gameObject.SetActive(false);
        }
    }

    public bool IsMenuPlayerEnable() {
        return MenuPlayer.activeSelf;
    }
}