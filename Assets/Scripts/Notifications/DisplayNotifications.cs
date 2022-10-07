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

    string[] allNotifications;

    void Start() {
        Server.username = "jooj";
        foreach(NotificationIcon icon in icons)
            icon.gameObject.SetActive(false);
        StartCoroutine(OpenDisplay());
    }

    IEnumerator OpenDisplay() {
        yield return StartCoroutine(GetNumberNotifications());
        for(int i = 0; i < numberMessages; i++)
            icons[i].gameObject.SetActive(true);
        if(numberMessages < icons.Length) {
            previousButton.interactable = false;
            nextButton.interactable = false;
        } else {
            previousButton.interactable = false;
            nextButton.interactable = true;            
        }
        yield return StartCoroutine(GetNotifications());
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
                
                List<string> id = new List<string>();
                List<string> titles = new List<string>();
                List<string> dates = new List<string>();
                List<string> issuer = new List<string>();
                List<string> status = new List<string>();
                List<string> type = new List<string>();

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
                break;
            default:
                break;
        }        
    }
}