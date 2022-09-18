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

    public void StartRoom(string roomName) {
        PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions() { MaxPlayers = 10 }, null);
    }

    public override void OnJoinedRoom() {
        //PhotonNetwork.LoadLevel("MainMap");
        foreach(Button b in buttons) {
            b.interactable = false;
        }
        transition.FadeInLevel("MainMap");
        Server.canMove = false;
    }
}