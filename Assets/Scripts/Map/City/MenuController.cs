using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MenuController : MonoBehaviour
{
    public GameObject Wind;
    public GameObject MenuPlayer;

    /*
    void Start() {
        CloseWind();
        CloseMenuPlayer();
    }
    */
    
    public void OpenWind() {
        Wind.SetActive(true);
    }

    public void CloseWind () {
        Wind.SetActive(false);
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