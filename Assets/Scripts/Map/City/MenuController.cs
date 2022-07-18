using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject MenuNews;
    public GameObject MenuConfig;
    public GameObject MenuNotif;
    public GameObject MenuMap;

    public GameObject MenuPlayer;
    public GameObject menuEmoji;
    public GameObject menuStudent;
    public GameObject menuShop;

    public GameObject black;

    /*
    void Start() {
        CloseWind();
        CloseMenuPlayer();
    }
    */

    // News
    public void OpenMenuNews() {
        MenuNews.SetActive(true);
    }

    public void CloseMenuNews () {
        MenuNews.SetActive(false);
    }

    // Configurations
    public void OpenMenuConfig() {
        MenuConfig.SetActive(true);
    }

    public void CloseMenuConfig () {
        MenuConfig.SetActive(false);
    }

    // Notifications
    public void OpenMenuNotif() {
        MenuNotif.SetActive(true);
    }

    public void CloseMenuNotif () {
        MenuNotif.SetActive(false);
    }

    // Map
    public void OpenMenuMap() {
        MenuMap.SetActive(true);
    }

    public void CloseMenuMap () {
        MenuMap.SetActive(false);
    }

    public void SendMessage() {
        ChatManager c = GameObject.Find(Server.username).GetComponent<ChatManager>();
        c.StartMessage();
    }

    public void OpenMenuPlayer () {
        MenuPlayer.SetActive(true);
        black.SetActive(true);
        PlayerMoving(false);
        foreach(Transform child in MenuPlayer.transform) {
            child.gameObject.SetActive(true);
        }
    }

    public void CloseMenuPlayer () {
        MenuPlayer.SetActive(false);
        black.SetActive(false);
        PlayerMoving(true);
        foreach(Transform child in MenuPlayer.transform) {
            child.gameObject.SetActive(false);
        }
    }

    public void OpenShop() {
        menuShop.SetActive(true);
        black.SetActive(true);
        menuEmoji.SetActive(false);
    }

    public void CloseShop() {
        menuShop.SetActive(false);
        black.SetActive(false);
    }

    // Student
    public void OpenMenuStudent() {
        menuStudent.SetActive(true);
        black.SetActive(true);
    }

    public void PlayerMoving(bool value) {
        Server.canMove = value;
    }

    public void CloseMenuStudent() {
        menuStudent.SetActive(false);
        black.SetActive(false);
        PlayerMoving(true);
    }

    public void OpenOrCloseMenuEmoji() {
        if(menuEmoji.activeSelf) menuEmoji.SetActive(false);
        else menuEmoji.SetActive(true);
        Server.canMove = !menuEmoji.activeSelf;
    }

    public void StartSendEmoji(int id) {
        StartCoroutine(ReturnPlayer(id));
    }

    IEnumerator ReturnPlayer(int id) {
        ChatManager c = GameObject.Find(Server.username).GetComponent<ChatManager>();
        c.SendEmoji(id);
        menuEmoji.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        Server.canMove = true;
    }

    public bool IsMenuPlayerEnabled() {
        return MenuPlayer.activeSelf;
    }

    public bool IsMenuEmojiEnabled() {
        return menuEmoji.activeSelf;
    }

    public bool IsAllMenusEnabled() {
        if(menuEmoji.activeSelf || MenuPlayer.activeSelf) return true;
        else return false;
    }
}