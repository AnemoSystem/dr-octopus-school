using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public GameObject menuUIFriend;
    public GameObject menuLibrary;

    public GameObject black;
    public GameObject book;
    public GameObject windowConfirm;

    /*
    void Start() {
        CloseWind();
        CloseMenuPlayer();
    }
    */

    // Menu Friend
    public void OpenMenuFriend(CustomBodyPart cs, string playerName) {
        PlayerMoving(false);
        black.SetActive(true);
        menuFriend.SetActive(true);
        ShowMenuFriend sf = menuFriend.GetComponent<ShowMenuFriend>();
        sf.SetFriend(cs);
        sf.SetFriendName(playerName);
    }

    public void SimpleOpenMenuFriend() {
        PlayerMoving(false);
        black.SetActive(true);
        menuFriend.SetActive(true);
    }

    public void CloseMenuFriend() {
        PlayerMoving(true);
        menuFriend.SetActive(false);
        black.SetActive(false);        
    }

    // Menu UI Friend (List of Friends)
    public void OpenUIFriend(bool status) {
        PlayerMoving(!status);
        menuUIFriend.SetActive(status);
        black.SetActive(status);
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
        PlayerMoving(false);
    }

    public void CloseMenuSettings() {
        MenuSettings.SetActive(false);
        black.SetActive(false);
        PlayerMoving(true);
    }

    // Notifications
    public void OpenMenuNotif() {
        MenuNotif.SetActive(true);
        black.SetActive(true);
        PlayerMoving(false);
    }

    public void CloseMenuNotif () {
        MenuNotif.SetActive(false);
        black.SetActive(false);
        PlayerMoving(true);
    }

    // Map
    public void OpenMenuMap() {
        MenuMap.SetActive(true);
        black.SetActive(true);
        PlayerMoving(false);
    }

    public void CloseMenuMap () {
        MenuMap.SetActive(false);
        black.SetActive(false);
        PlayerMoving(true);
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
        PlayerMoving(false);
    }

    public void CloseWindowConfirm() {
        windowConfirm.SetActive(false);
        black.SetActive(false);
        PlayerMoving(true);
    }

    public void OpenLibrary(bool status) {
        menuLibrary.SetActive(status);
        black.SetActive(status);
        PlayerMoving(!status);
    }

    public void OpenBook(bool status) {
        book.SetActive(status);
        black.SetActive(status);
        PlayerMoving(!status);        
    }
}