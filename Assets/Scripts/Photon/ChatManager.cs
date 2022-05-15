using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ChatManager : MonoBehaviour, Photon.Pun.IPunObservable
{
    private InputField chatInput;
    public Text playerText;
    PhotonView view;
    
    void Start() {
        chatInput = GameObject.Find("ChatInputField").GetComponent<InputField>();
        view = GetComponent<PhotonView>();
    }

    void Update() {
        if(view.IsMine) {
            if(Input.GetKeyDown(KeyCode.LeftShift) && chatInput.isFocused) {
                SendMessage();
            }
        }
    }

    void SendMessage() {
        StopCoroutine("Remove");
        playerText.text = chatInput.text;
        chatInput.text = "";
        StartCoroutine("Remove");
    }

    IEnumerator Remove() {
        yield return new WaitForSeconds(4f);
        playerText.text = "";
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if(stream.IsWriting) {
            stream.SendNext(playerText.text);
        } else if(stream.IsReading) {
            playerText.text = (string)stream.ReceiveNext();
        }
    }
}
