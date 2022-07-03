using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ChatManager : MonoBehaviour, Photon.Pun.IPunObservable
{
    private InputField chatInput;
    public Text playerText;
    public Text username;
    public GameObject ballon;
    PhotonView view;
    
    public GameObject ballonEmoji;
    public SpriteRenderer baseEmoji;
    public Sprite[] emojis;

    void Start() {
        chatInput = GameObject.Find("ChatInputField").GetComponent<InputField>();
        view = GetComponent<PhotonView>();

        if(view.IsMine) username.text = Server.username;
    }

    void Update() {
        //if(Input.GetKeyDown(KeyCode.LeftShift) && chatInput.isFocused) {
        if(chatInput.text == " ") chatInput.text = "";
        
        if(Input.GetKeyDown(KeyCode.Return))
            StartMessage();
    }

    public void StartMessage() {
        if(view.IsMine && chatInput.text != "") {
            SendMessage();
        }
    }

    void SendMessage() {
        StopCoroutine("Remove");
        ballon.SetActive(true);
        ballonEmoji.SetActive(false);
        playerText.text = chatInput.text;
        chatInput.text = "";
        StartCoroutine("Remove");
    }

    public void SendEmoji(int id) {
        StopCoroutine("Remove");
        ballon.SetActive(false);
        ballonEmoji.SetActive(true);
        baseEmoji.sprite = emojis[id];
        StartCoroutine("Remove");
    }

    IEnumerator Remove() {
        yield return new WaitForSeconds(4f);
        playerText.text = "";
        ballon.SetActive(false);
        ballonEmoji.SetActive(false);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if(stream.IsWriting) {
            stream.SendNext(playerText.text);
            stream.SendNext(username.text);
            stream.SendNext(ballon.activeSelf);
        } else if(stream.IsReading) {
            playerText.text = (string)stream.ReceiveNext();
            username.text = (string)stream.ReceiveNext();
            ballon.SetActive((bool)stream.ReceiveNext());
        }
    }
}