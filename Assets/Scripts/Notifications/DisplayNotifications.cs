using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class DisplayNotifications : MonoBehaviour
{
    public NotificationIcon[] icons;
    public Button previousButton;
    public Button nextButton;
    public Sprite[] spritesIcon;
    public Sprite[] spritesStatus;

    private int numberMessages;
    private int total;

    string[] allNotifications;
    List<string> id = new List<string>();

    void Start() {
        //Server.username = "jooj";
        foreach(NotificationIcon icon in icons)
            icon.gameObject.SetActive(false);
        StartCoroutine(OpenDisplay(0, "I"));
    }

    void ActivateButtons(bool next, bool prev) {
        previousButton.interactable = prev;
        nextButton.interactable = next; 
    }

    IEnumerator OpenDisplay(int from_id, string from_type) {
        yield return StartCoroutine(GetNumberNotifications(from_id, from_type));
        if(numberMessages <= 4) {
            foreach(NotificationIcon icon in icons)
                icon.gameObject.SetActive(false);
            
            for(int i = 0; i < numberMessages; i++)
                icons[i].gameObject.SetActive(true);
        } else {
            foreach(NotificationIcon icon in icons)
                icon.gameObject.SetActive(true);
        }
        if(from_type == "I") {
            if(numberMessages < icons.Length) ActivateButtons(false, false);
            else ActivateButtons(true, false);
        } else {
            if(numberMessages < 4) ActivateButtons(false, true);
            else ActivateButtons(true, true);
        }
        yield return StartCoroutine(GetNotifications(from_id, from_type));
    }

    IEnumerator GetNumberNotifications(int from_id, string from_type) {
        WWWForm form = new WWWForm();
        form.AddField("username", Server.username);
        form.AddField("from_id", from_id);
        form.AddField("from_type", from_type);

        UnityWebRequest www = UnityWebRequest.Post(
            Server.mainServer + "/unity/notifications/get_number_notifications.php", 
            form);
        www.certificateHandler = new BypassCertificate();
        yield return www.SendWebRequest();

        switch(www.result) {
            case UnityWebRequest.Result.ConnectionError:
                Debug.Log("Get Data - Connection Error");
                break;
            case UnityWebRequest.Result.DataProcessingError:
                Debug.Log("Get Data - Data Processing Error");
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.Log("Get Data - HTTP Error");
                break;
            case UnityWebRequest.Result.Success:
                string result = www.downloadHandler.text.ToString();
                numberMessages = Convert.ToInt32(result);
                if(from_type == "I") total = numberMessages;
                break;
            default:
                break;
        }
    }

    IEnumerator GetNotifications(int from_id, string from_type) {
        WWWForm form = new WWWForm();
        form.AddField("username", Server.username); 
        form.AddField("from_id", from_id);
        form.AddField("from_type", from_type);

        UnityWebRequest www = UnityWebRequest.Post(
            Server.mainServer + "/unity/notifications/get_notifications.php", 
            form);
        www.certificateHandler = new BypassCertificate();
        yield return www.SendWebRequest();

        switch(www.result) {
            case UnityWebRequest.Result.ConnectionError:
                Debug.Log("Get Data - Connection Error");
                break;
            case UnityWebRequest.Result.DataProcessingError:
                Debug.Log("Get Data - Data Processing Error");
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.Log("Get Data - HTTP Error");
                break;
            case UnityWebRequest.Result.Success:
                string result = www.downloadHandler.text.ToString();
                allNotifications = result.Split('%');
                
                List<string> titles = new List<string>();
                List<string> dates = new List<string>();
                List<string> issuer = new List<string>();
                List<string> status = new List<string>();
                List<string> type = new List<string>();
                id.Clear();

                for(int i = 0; i < allNotifications.Length; i++) {
                    string[] t = allNotifications[i].Split('@');
                    for(int j = 0; j < t.Length; j++) {   
                        switch(j) {
                            case 0:
                                id.Add(t[j]);
                                break;
                            case 1:
                                titles.Add(t[j]);
                                break;
                            case 2:
                                type.Add(t[j]);
                                break;
                            case 3:
                                issuer.Add(t[j]);
                                break;
                            case 4:
                                dates.Add(Message.FormatDate(t[j]));
                                break;
                            case 5:
                                status.Add(t[j]);
                                break;
                            default:
                                break;
                        }   
                    }
                }

                if(from_type == "C") {
                    id.Reverse();
                    titles.Reverse();
                    type.Reverse();
                    issuer.Reverse();
                    dates.Reverse();
                    status.Reverse();
                }

                for(int i = 0; i < icons.Length; i++) {
                    // Change icon type message
                    switch(type[i]) {
                        case "M":
                            icons[i].icon.sprite = spritesIcon[0];
                            break;
                        case "P":
                            icons[i].icon.sprite = spritesIcon[1];
                            break;
                        case "F":
                            icons[i].icon.sprite = spritesIcon[2];
                            break;
                        default:
                            break;
                    }

                    // Change icon (status message)
                    if(status[i] == "R") icons[i].statusIcon.sprite = spritesStatus[1];
                    else icons[i].statusIcon.sprite = spritesStatus[0];

                    // Rest of information
                    icons[i].messageTitle.text = titles[i];
                    icons[i].messageDate.text = dates[i];
                    icons[i].issuerUsername.text = issuer[i];
                }
                id.RemoveAt(id.Count - 1);
                //id.ForEach(e => Debug.Log(e));
                //if(total == numberMessages) ActivateButtons(true, false);
                break;
            default:
                break;
        }        
    }

    public void NextNotifications() {
        var temp = Convert.ToInt32(id[id.Count - 1]);
        //id.ForEach(e => Debug.Log(e));
        StartCoroutine(OpenDisplay(temp, "D"));
    }

    public void PreviousNotifications() {
        var temp = Convert.ToInt32(id[0]);
        StartCoroutine(OpenDisplay(temp, "C"));
    }
}