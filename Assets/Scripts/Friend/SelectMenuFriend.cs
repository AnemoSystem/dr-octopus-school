using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMenuFriend : MonoBehaviour
{
    public Text username;
    public CustomBodyPart controller;

    public void SetBodyParts(int s, int t, int l, int h) {
        controller.idSkin = s;
        controller.idLegs = l;
        controller.idTorso = t;
        controller.idHair = h;
    }

    public void SetUsername(string user) {
        username.text = user;
    }

    public void Open(bool status) {
        gameObject.SetActive(status);
    }
}