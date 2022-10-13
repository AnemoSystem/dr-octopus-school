using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

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

    public static void SendMessage(
        string title, string message, 
        string type, string status,
        string from, string to, MonoBehaviour whocalls
    ) {
        whocalls.StartCoroutine(StartSendMessage(
            title, message, type, status, from, to
        ));
    }

    public static IEnumerator StartSendMessage(
        string title, string message, 
        string type, string status,
        string from, string to
    ) {
        WWWForm form = new WWWForm();
        form.AddField("title", title);
        form.AddField("message", message);
        form.AddField("type", type);
        form.AddField("status", status);
        form.AddField("from", from);
        form.AddField("to", to);

        UnityWebRequest www = UnityWebRequest.Post(Server.mainServer + 
        "/school-management-system/unity/notifications/send_message.php", form);
        yield return www.SendWebRequest();
        switch (www.result)
        {
            case UnityWebRequest.Result.ConnectionError:
                Debug.Log("Connection Error");
                break;
            case UnityWebRequest.Result.DataProcessingError:
                Debug.Log("Data Processing Error");
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.Log("HTTP Error");
                break;
            case UnityWebRequest.Result.Success:
                Debug.Log("Send message with success!");
                break;
            default:
                break;
        }
    }
}