using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddFriend : MonoBehaviour
{
    private MenuController menuController;

    void Start() {
        menuController = GameObject.Find("MenuController").GetComponent<MenuController>();
    }

    void OnMouseDown() {
        if(gameObject.name != Server.username)
            menuController.OpenMenuFriend();
    }
}