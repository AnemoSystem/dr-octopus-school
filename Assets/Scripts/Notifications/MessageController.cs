using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageController : MonoBehaviour
{ 
    public SelectMenuFriend selectMenu;

    [Header("Message Section")]
    public GameObject menuMessage;
    public Text from;
    public Text to;
    public InputField title;
    public InputField message;

    public void OpenMessageMenu(bool status) {
        menuMessage.SetActive(status);
        selectMenu.gameObject.SetActive(!status);
        if(status) {
            from.text = "De:\n" + Server.username;
            to.text = "Para:\n" + selectMenu.username.text;
        }
    }

    public void SendSimpleMessage(bool forSchool) {
        string type = forSchool ? "P" : "S";
        DisplayMessage.SendMessage(
            title.text,
            message.text,
            type,
            "N",
            Server.username,
            selectMenu.username.text,
            this
        );
    }

    void OnDisable() {
        OpenMessageMenu(false);
    }
}
