using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CustomBodyPartMenu : MonoBehaviour
{
    public CustomBodyPart playerBodyPart;
    public CustomBodyPart menuBodyPart;
    PhotonView view;

    void Start() {
        //view = GameObject.Find("PhotonViewController").GetComponent<PhotonView>();
        menuBodyPart = transform.GetChild(3).GetComponent<CustomBodyPart>();
        playerBodyPart = GameObject.FindGameObjectsWithTag("Player")[0].transform.GetChild(4).GetComponent<CustomBodyPart>();
    }

    public void UpdateMenuBodyParts() {
        menuBodyPart.idSkin = playerBodyPart.idSkin;
        menuBodyPart.idTorso = playerBodyPart.idTorso;
        menuBodyPart.idLegs = playerBodyPart.idLegs;
        menuBodyPart.idHair = playerBodyPart.idHair;
    }

    public void UpdatePlayerBodyParts() {
        playerBodyPart.idSkin = menuBodyPart.idSkin;
        playerBodyPart.idTorso = menuBodyPart.idTorso;
        playerBodyPart.idLegs = menuBodyPart.idLegs;
        playerBodyPart.idHair = menuBodyPart.idHair;
    }
}