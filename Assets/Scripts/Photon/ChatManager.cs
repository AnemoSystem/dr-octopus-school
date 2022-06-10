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
    
    void Start() {
        chatInput = GameObject.Find("ChatInputField").GetComponent<InputField>();
        view = GetComponent<PhotonView>();

        if(view.IsMine) username.text = Server.username;
    }

    void Update() {
        //if(Input.GetKeyDown(KeyCode.LeftShift) && chatInput.isFocused) {
        if(Input.GetKeyDown(KeyCode.Return) && chatInput.text != "")
            StartMessage();
    }

    void StartMessage() {
        if(view.IsMine) {
            SendMessage();
            if(chatInput.text == " ") chatInput.text = "";
        }
    }

    void SendMessage() {
        StopCoroutine("Remove");
        ballon.SetActive(true);
        playerText.text = chatInput.text;
        chatInput.text = "";
        StartCoroutine("Remove");
    }

    IEnumerator Remove() {
        yield return new WaitForSeconds(4f);
        playerText.text = "";
        ballon.SetActive(false);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if(stream.IsWriting) {
            stream.SendNext(playerText.text);
            stream.SendNext(username.text);
        } else if(stream.IsReading) {
            playerText.text = (string)stream.ReceiveNext();
            username.text = (string)stream.ReceiveNext();
        }
    }
}