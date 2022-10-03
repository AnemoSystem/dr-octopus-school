using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject MenuNews;
    public GameObject MenuSettings;
    public GameObject MenuNotif;
    public GameObject MenuMap;

    public GameObject MenuPlayer;
    public GameObject menuEmoji;
    public GameObject menuStudent;
    public GameObject menuShop;
    public GameObject menuFriend;

    public GameObject black;
    public GameObject windowConfirm;

    /*
    void Start() {
        CloseWind();
        CloseMenuPlayer();
    }
    */

    // Menu Friend
    public void OpenMenuFriend() {
        menuFriend.SetActive(true);
        black.SetActive(true);
    }

    public void CloseMenuFriend() {
        menuFriend.SetActive(false);
        black.SetActive(false);        
    }

    // News
    public void OpenMenuNews() {
        //MenuNews.SetActive(true);
        Application.OpenURL("https://gggcd-tcc.github.io/download-site/blog.html");
    }

    public void CloseMenuNews () {
        MenuNews.SetActive(false);
    }

    // Settings
    public void OpenMenuSettings() {
        MenuSettings.SetActive(true);
        black.SetActive(true);
    }

    public void CloseMenuSettings() {
        MenuSettings.SetActive(false);
        black.SetActive(false);
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
        black.SetActive(true);
    }

    public void CloseMenuMap () {
        MenuMap.SetActive(false);
        black.SetActive(false);
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

    // Shop
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

    /*
    public void Test() {
        SceneManager.LoadScene("Runner");
        GameObject o = GameObject.Find(Server.username);
        o.transform.position = new Vector2(-20, -20);
    }
    */

    public void OpenWindowConfirm() {
        windowConfirm.SetActive(true);
        black.SetActive(true);
    }

    public void CloseWindowConfirm() {
        windowConfirm.SetActive(false);
        black.SetActive(false);
    }
}