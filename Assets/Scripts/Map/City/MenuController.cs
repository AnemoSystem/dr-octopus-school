using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MenuController : MonoBehaviour
{
    public GameObject MenuNews;
    public GameObject MenuConfig;
    public GameObject MenuNotif;
    public GameObject MenuMap;

    public GameObject MenuPlayer;

    /*
    void Start() {
        CloseWind();
        CloseMenuPlayer();
    }
    */
    
// News
    public void OpenMenuNews() {
        MenuNews.SetActive(true);
    }

    public void CloseMenuNews () {
        MenuNews.SetActive(false);
    }

// Configurations
    public void OpenMenuConfig() {
        MenuConfig.SetActive(true);
    }

    public void CloseMenuConfig () {
        MenuConfig.SetActive(false);
    }

// Notifications
    public void OpenMenuNotif() {
        MenuNotif.SetActive(true);
    }

    public void CloseMenuNotif () {
        MenuNotif.SetActive(false);
    }

// Map
    public void OpenMenuMap() {
        MenuMap.SetActive(true);
    }

    public void CloseMenuMap () {
        MenuMap.SetActive(false);
}
    public void SendMessage() {
        ChatManager c = GameObject.Find(Server.username).GetComponent<ChatManager>();
        c.StartMessage();
    }

    public void OpenMenuPlayer () {
        MenuPlayer.SetActive(true);
        foreach(Transform child in MenuPlayer.transform) {
            child.gameObject.SetActive(true);
        }
    }

    public void CloseMenuPlayer () {
        MenuPlayer.SetActive(false);
        foreach(Transform child in MenuPlayer.transform) {
            child.gameObject.SetActive(false);
        }
    }

    public void CloseGame() {
        StartCoroutine(Disconnect());
    }

    public bool IsMenuPlayerEnable() {
        return MenuPlayer.activeSelf;
    }

    IEnumerator Disconnect() {
        WWWForm form = new WWWForm();
        form.AddField("username", Server.username);

        UnityWebRequest www = UnityWebRequest.Post("https://revisory-claws.000webhostapp.com/unity/disconnect_login.php", form);
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
                Debug.Log("Disconnect with success");
                Application.Quit();
                break;
        }
        www.Dispose();        
    }
}