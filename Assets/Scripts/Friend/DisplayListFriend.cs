using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayListFriend : MonoBehaviour
{
    private string id;
    public Image statusIcon;
    public Text friend;

    public void SetOnline() {
        statusIcon.color = new Color(1f, 1f, 1f, 1f);
    }

    public void SetOffline() {
        statusIcon.color = new Color(1f, 1f, 1f, 0.5f);
    }

    public void SetFriendName(string name) {
        friend.text = name;
    }

    public void SetID(string i) {
        id = i;
    }
}
