using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ChatManager : MonoBehaviour, Photon.Pun.IPunObservable
{
    [SerializeField]
    private InputField chatInput;
    
    public Text playerText;
    public Text username;
    public GameObject ballon;
    PhotonView view;
    
    public GameObject ballonEmoji;
    public SpriteRenderer baseEmoji;
    public Sprite[] emojis;
    private int selectedEmoji;

    void Start() {
        view = GetComponent<PhotonView>();
        if(view.IsMine) username.text = Server.username;
        StartCoroutine(WaitChatInput());
    }

    IEnumerator WaitChatInput() {
        yield return new WaitForSeconds(2f);
        chatInput = GameObject.Find("ChatInputField").GetComponent<InputField>();
        chatInput.onValueChanged.AddListener(delegate {ValueChangeCheck(); });
    }

    void Update() {
        //if(Input.GetKeyDown(KeyCode.LeftShift) && chatInput.isFocused) {
        if(Input.GetKeyDown(KeyCode.Return))
            StartMessage();
        
        baseEmoji.sprite = emojis[selectedEmoji];
    }

    public void ValueChangeCheck()
    {
        if(chatInput.text == " ") chatInput.text = "";
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
        //baseEmoji.sprite = emojis[id];
        selectedEmoji = id;
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
            stream.SendNext(ballonEmoji.activeSelf);
            stream.SendNext(selectedEmoji);
        } else if(stream.IsReading) {
            playerText.text = (string)stream.ReceiveNext();
            username.text = (string)stream.ReceiveNext();
            ballon.SetActive((bool)stream.ReceiveNext());
            ballonEmoji.SetActive((bool)stream.ReceiveNext());
            selectedEmoji = (int)stream.ReceiveNext();
        }
    }
}