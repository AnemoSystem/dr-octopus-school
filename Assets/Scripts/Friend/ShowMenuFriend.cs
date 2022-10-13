using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMenuFriend : MonoBehaviour
{
    public CustomBodyPart customBody;
    private CustomBodyPart friend;
    public Text friendName;
    public GameObject windowConfirm;
    public GameObject UIMessage;
    public InputField inputMessage;
    public GameObject playerView;
    public GameObject windowResults;

    [Header("Message Itens")]
    public Text title;
    public Text from;
    public Text to;

    public void OpenWindow() {
        playerView.SetActive(false);
        windowConfirm.SetActive(true);
    }
    
    public void CloseWindow() {
        playerView.SetActive(true);
        windowConfirm.SetActive(false);
    }

    public void OpenMenuMessage() {
        inputMessage.text = "";
        title.text = "Título: Pedido de amizade para " + friendName.text;
        from.text = "De:\n" + Server.username;
        to.text = "Para:\n" + friendName.text;
        CloseWindow();
        playerView.SetActive(false);
        UIMessage.SetActive(true);
    }

    public void CloseMenuMessage() {
        OpenWindowResults(false);
        UIMessage.SetActive(false);
        playerView.SetActive(true);
    }

    public void OpenWindowResults(bool value) {
        windowResults.SetActive(value);
        if(!value) CloseMenuMessage();
    }

    void OnEnable() {
        playerView.SetActive(true);
        OpenWindowResults(false);
        CloseWindow();
        CloseMenuMessage();
    }

    void Update() {
        SearchParts();
    }
    
    void SearchParts() {
        customBody.idSkin = friend.idSkin;
        customBody.idLegs = friend.idLegs;
        customBody.idTorso = friend.idTorso;
        customBody.idHair = friend.idHair;
    }

    public void SetFriend(CustomBodyPart cs) {
        friend = cs;
    }

    public void SetFriendName(string name) {
        friendName.text = name;
    }

    public void SendFriendRequest() {
        DisplayMessage.SendMessage(
            title.text,
            inputMessage.text,
            "F",
            "N",
            Server.username,
            friendName.text,
            this
        );
        OpenWindowResults(true);
    }
}