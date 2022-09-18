using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ChangeRoom : MonoBehaviourPunCallbacks
{
    public LoadWithTransition transition;
    public string whichScene;

    void OnTriggerEnter2D(Collider2D other) {
        //transition.FadeIn(whichScene);
        PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.player);
        PhotonNetwork.LeaveRoom();
        Debug.Log("encontrou");
    }

    void OnLeftRoom() {
        transition.FadeInRoom(whichScene);
    }
}