using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ChangeRoom : MonoBehaviourPunCallbacks
{
    public LoadWithTransition transition;
    public string whichScene;
    public bool isIndividualScene = false;
    public TransitionLoading loadingTransition;

    private string roomName;
    public GameObject[] anotherTriggers;

    void Start() {
        roomName = whichScene;
    }

    public void ChangeScene() {
        Server.canMove = false;
        StartCoroutine(StartChanging());
    }

    void OnTriggerEnter2D(Collider2D other) {
        //transition.FadeIn(whichScene);
        //PhotonNetwork.DestroyPlayerObjects(1);
        /*
        Reference r = other.gameObject.GetComponent<Reference>();
        if(r.usernameRef.text == Server.username) ChangeScene();
        */
        PhotonView ph = other.gameObject.GetComponent<PhotonView>();
        if(ph.IsMine) ChangeScene();
    }

    IEnumerator StartChanging() {
        loadingTransition.FadeIn();
        yield return new WaitForSeconds(0.4f);
        Server.canMove = true;
        PhotonNetwork.LeaveRoom();
        while(PhotonNetwork.InRoom)
            yield return null;
        foreach(GameObject a in anotherTriggers)
            a.SetActive(false);
        //Debug.Log("encontrou");
    }

    public override void OnLeftRoom() {
        if(isIndividualScene)
            roomName += ("_" + Server.username);
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnJoinedLobby() {
        PhotonNetwork.JoinOrCreateRoom(
            Server.idServer + roomName, new RoomOptions() { MaxPlayers = 10 }, null
        );
    }

    public override void OnConnectedToMaster() {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedRoom() {
        transition.FadeInLevel(whichScene);
    }
}