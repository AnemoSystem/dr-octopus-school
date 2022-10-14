using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Message
{
    public string title;
    [TextArea(3, 10)]
    public string fullMessage;
    public string date;
    public string issuer;

    public static string FormatDate(string date) {
        string[] d = date.Split('-');
        string result = d[2] + "/" + d[1] + "/" + d[0];
        return result;
    }
}