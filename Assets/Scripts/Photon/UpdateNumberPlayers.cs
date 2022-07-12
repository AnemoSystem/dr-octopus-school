using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class UpdateNumberPlayers : MonoBehaviour
{
    public Text buttonTextA;
    public Text buttonTextB;

    void Update()
    {
        buttonTextA.text = "Sala A - " + Server.playersRoomA + "/10";
        buttonTextB.text = "Sala B - " + Server.playersRoomB + "/10";
    }
}