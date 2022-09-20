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

    void Start() {
        roomName = whichScene;
    }

    void OnTriggerEnter2D(Collider2D other) {
        //transition.FadeIn(whichScene);
        //PhotonNetwork.DestroyPlayerObjects(Server.username);
        StartCoroutine(StartChanging());
    }

    IEnumerator StartChanging() {
        loadingTransition.FadeIn();
        yield return new WaitForSeconds(0.4f);
        PhotonNetwork.LeaveRoom();
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