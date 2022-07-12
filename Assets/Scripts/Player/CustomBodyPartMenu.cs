using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomBodyPartMenu : MonoBehaviour
{
    public CustomBodyPart playerBodyPart;
    public CustomBodyPart menuBodyPart;

    void Start() {
        menuBodyPart = transform.GetChild(3).GetComponent<CustomBodyPart>();
    }

    void OnEnable() {
        FindPlayer();
        UpdateMenuBodyParts();
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
        playerBodyPart.UpdateIDsArray();
    }

    public void FindPlayer() {
        playerBodyPart = GameObject.Find(Server.username + "/BodyPartController").GetComponent<CustomBodyPart>();
    }
}