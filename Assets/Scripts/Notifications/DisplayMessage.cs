using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayMessage : MonoBehaviour
{
    public Text title;
    public Text message;
    public Text date;
    public Text issuer;

    public void SetMessage(string t, string m, string i, string d) {
        title.text = t;
        message.text = m;
        date.text = Message.FormatDate(d);
        issuer.text = i;
    }
}