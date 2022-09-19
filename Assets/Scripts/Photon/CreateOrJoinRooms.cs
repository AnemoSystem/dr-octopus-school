using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class CreateOrJoinRooms : MonoBehaviourPunCallbacks
{
    public LoadWithTransition transition;
    public Button[] buttons;
    public GameObject loadingIndicator;

    public void StartRoom(string roomName) {
        loadingIndicator.SetActive(true);
        foreach(Button b in buttons) {
            b.interactable = false;
        }
        PhotonNetwork.JoinOrCreateRoom(roomName + "_MainMap", new RoomOptions() { MaxPlayers = 10 }, null);
        if(roomName == "A")
            Server.idServer = "A";
        else
            Server.idServer = "B";
    }

    public override void OnJoinedRoom() {
        //PhotonNetwork.LoadLevel("MainMap");
        transition.FadeInLevel("MainMap");
        Server.canMove = false;
    }
}