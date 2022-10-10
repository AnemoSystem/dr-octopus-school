using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;

public class DisplayNotificationsScrollable : MonoBehaviour
{
    public GameObject recieveList;
    public GameObject simpleMessage;

    public GameObject template;
    public Transform parent;
    private int numberMessages;
    public Sprite[] spritesIcon;
    public Sprite[] spritesStatus;

    string[] allNotifications;
    List<string> id = new List<string>();
    List<string> titles = new List<string>();
    List<string> dates = new List<string>();
    List<string> issuer = new List<string>();
    List<string> status = new List<string>();
    List<string> type = new List<string>();
    
    void Start() {
        Server.username = "jooj";
        StartCoroutine(StartDisplay());
    }

    IEnumerator StartDisplay() {
        yield return StartCoroutine(GetNumberNotifications());
        yield return StartCoroutine(GetNotifications());
        /*
        for(int i = 0; i < numberMessages; i++) {
            Instantiate(template, parent);
        }
        */
    }

    IEnumerator GetNumberNotifications() {
        WWWForm form = new WWWForm();
        form.AddField("username", Server.username);

        UnityWebRequest www = UnityWebRequest.Post(
            Server.mainServer + "/school-management-system/unity/notifications/get_number_notifications.php", 
            form);
        
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
                //Debug.Log(result);
                numberMessages = Convert.ToInt32(result);
                break;
            default:
                break;
        }
    }

    IEnumerator GetNotifications() {
        WWWForm form = new WWWForm();
        form.AddField("username", Server.username); 

        UnityWebRequest www = UnityWebRequest.Post(
            Server.mainServer + "/school-management-system/unity/notifications/get_notifications.php", 
            form);
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

                for(int i = 0; i < numberMessages; i++) {
                    GameObject g = Instantiate(template, parent);
                    NotificationIcon n = g.GetComponent<NotificationIcon>();
                    // Change icon type message
                    switch(type[i]) {
                        case "M":
                            n.icon.sprite = spritesIcon[0];
                            break;
                        case "P":
                            n.icon.sprite = spritesIcon[1];
                            break;
                        case "F":
                            n.icon.sprite = spritesIcon[2];
                            break;
                        default:
                            break;
                    }

                    // Change icon (status message)
                    if(status[i] == "R") n.statusIcon.sprite = spritesStatus[1];
                    else n.statusIcon.sprite = spritesStatus[0];

                    // Rest of information
                    n.messageTitle.text = titles[i];
                    n.messageDate.text = dates[i];
                    n.issuerUsername.text = issuer[i];

                    n.SetID(id[i]);
                }
                id.RemoveAt(id.Count - 1);
                break;
            default:
                break;
        }        
    }

    public void OpenMessage() {
        
    }
}
