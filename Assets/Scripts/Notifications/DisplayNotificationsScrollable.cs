using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;
using UnityEngine.Events;

public class DisplayNotificationsScrollable : MonoBehaviour
{
    public GameObject recieveList;
    public DisplayMessage simpleMessage;
    public DisplayMessage addFriendMessage;

    public GameObject template;
    public Transform parent;
    private int numberMessages;
    public Sprite[] spritesIcon;
    public Sprite[] spritesStatus;
    public GameObject loading;

    List<string> id = new List<string>();
    List<string> titles = new List<string>();
    List<string> dates = new List<string>();
    List<string> issuer = new List<string>();
    List<string> status = new List<string>();
    List<string> type = new List<string>();
    
    public GameObject windowConfirm;
    public Text windowText;
    public Button yesButton;
    public Button noButton;

    public void OpenWindow(string t) {
        windowConfirm.SetActive(true);
        windowText.text = t;
    }

    public void CloseWindow() {
        windowConfirm.SetActive(false);
    }

    void Start() {
        Server.username = "jooj";

        CloseWindow();
        loading.SetActive(true);
        simpleMessage.gameObject.SetActive(false);
        recieveList.SetActive(true);
        
        ResetList();
    }

    void ResetList() {
        foreach(Transform child in parent) {
            Destroy(child.gameObject);
        }
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
                string[] sp = result.Split('%');

                List<string> allNotifications = new List<string>();

                id.Clear();
                titles.Clear();
                type.Clear();
                issuer.Clear();
                dates.Clear();
                status.Clear();
                //allNotifications.Clear();

                allNotifications.AddRange(sp);
                Debug.Log(string.Join(" ", allNotifications));
                for(int i = 0; i < allNotifications.Count; i++) {
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
                loading.SetActive(false);
                id.RemoveAt(id.Count - 1);
                break;
            default:
                break;
        }
        // fix bug
        if(parent.childCount != numberMessages) {
            int t = parent.childCount - numberMessages;
            for(int i = t; i < parent.childCount; i++) {
                Destroy(parent.GetChild(i).gameObject);
            }
        }      
    }

    IEnumerator SearchSimpleMessage(string myID) {
        WWWForm form = new WWWForm();
        form.AddField("id", myID); 
        
        loading.SetActive(true);

        UnityWebRequest www = UnityWebRequest.Post(
            Server.mainServer + "/school-management-system/unity/notifications/simple_message.php", 
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
                string[] details = result.Split('=');
                if(details[4] == "F") {
                    addFriendMessage.SetMessage(
                        details[0],
                        details[1],
                        details[2],
                        details[3]
                    );
                    addFriendMessage.gameObject.SetActive(true);
                } else {
                    simpleMessage.SetMessage(
                        details[0],
                        details[1],
                        details[2],
                        details[3]
                    );
                    simpleMessage.gameObject.SetActive(true);
                }
                loading.SetActive(false);
                break;
            default:
                break;
        }
    }

    IEnumerator DeleteNotification(string myID) {
        WWWForm form = new WWWForm();
        form.AddField("id", myID); 
        
        loading.SetActive(true);
        recieveList.SetActive(false);

        UnityWebRequest www = UnityWebRequest.Post(
            Server.mainServer + "/school-management-system/unity/notifications/delete_notifications.php", 
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
                loading.SetActive(false);
                recieveList.SetActive(true);
                Debug.Log("Removed with success!");
                break;
            default:
                break;
        }
        ResetList();
    }

    public void OpenMessage(string myID) {
        recieveList.SetActive(false);
        StartCoroutine(SearchSimpleMessage(myID));
    }

    public void CloseMessage() {
        simpleMessage.gameObject.SetActive(false);
        addFriendMessage.gameObject.SetActive(false);
        recieveList.SetActive(true);
        ResetList();
    }

    public void DeleteMessage(string myID) {
        OpenWindow("Você realmente quer deletar a mensagem?");
        yesButton.onClick.AddListener(delegate{StartDeleteMsg(myID);});
        noButton.onClick.AddListener(CloseWindow);
    }

    void StartDeleteMsg(string myID) {
        StartCoroutine(DeleteNotification(myID));
        CloseWindow();
    }
}
