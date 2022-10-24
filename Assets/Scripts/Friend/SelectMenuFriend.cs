using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class SelectMenuFriend : MonoBehaviour
{
    private string id;
    public Text username;
    public CustomBodyPart controller;
    public GameObject confirmDeleteWindow;
    public GameObject playerViewGameObject;

    public void SetBodyParts(int s, int t, int l, int h) {
        controller.idSkin = s;
        controller.idLegs = l;
        controller.idTorso = t;
        controller.idHair = h;
    }

    public void SetUsername(string user) {
        username.text = user;
    }

    public void SetID(string i) {
        id = i;
    }

    public void Open(bool status) {
        gameObject.SetActive(status);
    }

    public void OpenDeleteWindow(bool status) {
        confirmDeleteWindow.SetActive(status);
        playerViewGameObject.SetActive(!status);
    }

    public void DeleteFriend() {
        StartCoroutine(StartDeleteFriend(id));
        Open(false);
        OpenDeleteWindow(false);
    }

    IEnumerator StartDeleteFriend(string friendID) {
        WWWForm form = new WWWForm();
        form.AddField("id", friendID);
        Debug.Log(friendID);
        UnityWebRequest www = UnityWebRequest.Post(
            Server.mainServer + "/school-management-system/unity/remove_friend.php", 
            form
        );

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
                Debug.Log("Removed with success!");
                break;
        }
    }
}