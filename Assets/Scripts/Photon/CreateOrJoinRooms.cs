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

    private string[] roomNames = {"City", "StrangePlace", "SchoolEntrace", "MainMap"};
    private string roomSelected;

    public void StartRoom(string roomName) {
        int index = Random.Range(0, roomName.Length);
        roomSelected = roomNames[index];

        loadingIndicator.SetActive(true);
        foreach(Button b in buttons) {
            b.interactable = false;
        }

        if(roomName == "A")
            Server.idServer = "A";
        else
            Server.idServer = "B";

        PhotonNetwork.JoinOrCreateRoom(Server.idServer + "_" + roomSelected, new RoomOptions() { MaxPlayers = 10 }, null);
    }

    public override void OnJoinedRoom() {
        //PhotonNetwork.LoadLevel("MainMap");
        transition.FadeInLevel(roomSelected);
        Server.canMove = false;
    }
}