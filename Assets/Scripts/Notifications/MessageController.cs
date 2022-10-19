using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageController : MonoBehaviour
{ 
    public SelectMenuFriend selectMenu;
    public GameObject window;

    [Header("Message Section")]
    public GameObject menuMessage;
    public Text from;
    public Text to;
    public InputField title;
    public InputField message;

    public void OpenMessageMenu(bool forSchool) {
        menuMessage.SetActive(true);
        selectMenu.gameObject.SetActive(false);
        from.text = "De:\n" + Server.username;
        if(forSchool)
            to.text = "Para:\nEquipe da Escola";
        else
            to.text = "Para:\n" + selectMenu.username.text;
    }

    public void CloseMessageMenu() {
        menuMessage.SetActive(false);
        selectMenu.gameObject.SetActive(!true);
        window.SetActive(false);
        title.text = "";
        message.text = "";
    }

    public void SendSimpleMessage() {
        /*
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
        */
        if(to.text == "Para:\nEquipe da Escola") {
            DisplayMessage.SendMessage(
                title.text,
                message.text,
                "P",
                "N",
                Server.username,
                "admin",
                this
            );
        } else {
            DisplayMessage.SendMessage(
                title.text,
                message.text,
                "S",
                "N",
                Server.username,
                selectMenu.username.text,
                this
            );
        }
        window.SetActive(true);
    }

    void OnDisable() {
        OpenMessageMenu(false);
    }
}