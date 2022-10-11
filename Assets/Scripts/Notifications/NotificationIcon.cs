using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationIcon : MonoBehaviour
{
    public string id;
    public Image icon;
    public Image statusIcon;
    public Text messageTitle;
    public Text messageDate;
    public Text issuerUsername;
    public Button remove;
    public Button read;

    DisplayNotificationsScrollable dn;

    public void SetID(string i) {
        id = i;
    }

    public void OpenMessage() {
        dn = GameObject.Find("NotificationsController").GetComponent<DisplayNotificationsScrollable>();
        dn.OpenMessage(id);
    }

    public void DeleteMessage() {
        dn = GameObject.Find("NotificationsController").GetComponent<DisplayNotificationsScrollable>();
        dn.DeleteMessage(id);
    }
}
