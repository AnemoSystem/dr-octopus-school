using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

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
    public GameObject buttonFriend;

    [Header("Message Itens")]
    public Text title;
    public Text from;
    public Text to;

    void Start() {
        Reset();
    }

    public void OpenWindow() {
        playerView.SetActive(false);
        windowConfirm.SetActive(true);
    }
    
    public void CloseWindow() {
        playerView.SetActive(true);
        windowConfirm.SetActive(false);
    }
    
    public void OpenMenuMessage(bool status) {
        if(status) {
            inputMessage.text = "";
            title.text = "Pedido de amizade para " + friendName.text;
            from.text = "De:\n" + Server.username;
            to.text = "Para:\n" + friendName.text;
            CloseWindow();
            playerView.SetActive(false);
            UIMessage.SetActive(true);
        }
        else {
            playerView.SetActive(true);
            UIMessage.SetActive(false);            
        }
    }

    public void OpenWindowResults(bool value) {
        windowResults.SetActive(value);
        if(!value) Reset();
    }

    void OnEnable() {
        StartCoroutine(VerifyFriendship());
        Reset();
    }

    public void Reset() {
        playerView.SetActive(true);
        OpenWindowResults(false);
        CloseWindow();
        UIMessage.SetActive(false);
    }

    void Update() {
        SearchParts(friend.idSkin, friend.idLegs, friend.idTorso, friend.idHair);
    }
    
    void SearchParts(int skin, int legs, int torso, int hair) {
        customBody.idSkin = skin;
        customBody.idLegs = legs;
        customBody.idTorso = torso;
        customBody.idHair = hair;
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

    IEnumerator VerifyFriendship() {
        WWWForm form = new WWWForm();
        form.AddField("user_1", Server.username);
        form.AddField("user_2", friendName.text);

        UnityWebRequest www = UnityWebRequest.Post(
            Server.mainServer + "/unity/verify_friends.php", 
            form
        );
        www.certificateHandler = new BypassCertificate();
        yield return www.SendWebRequest();
        
        switch (www.result)
        {
            case UnityWebRequest.Result.ConnectionError:
                Debug.Log("Connection Error");
                break;
            case UnityWebRequest.Result.DataProcessingError:
                Debug.Log("Data Processing Error");
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.Log("HTTP Error");
                break;
            case UnityWebRequest.Result.Success:
                Debug.Log("Removed with success!");
                string result = www.downloadHandler.text.ToString();
                if(result == "exists") buttonFriend.SetActive(false);
                else buttonFriend.SetActive(true);
                break;
        }
    }
}