using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class UpdatePlayersCounter : MonoBehaviour
{   
    public void AddPlayerA() {
        StartCoroutine(Updating(0, "A"));
    }

    public void AddPlayerB() {
        StartCoroutine(Updating(0, "B"));
    }

    public void RemovePlayerA() {
        StartCoroutine(Updating(1, "A"));
    }

    public void RemovePlayerB() {
        StartCoroutine(Updating(1, "B"));
    }

    public void GetPlayerA() {
        StartCoroutine(Updating(2, "A"));
    }

    public void GetPlayerB() {
        StartCoroutine(Updating(2, "B"));
    }    

    IEnumerator Updating(int type, string room) {
        WWWForm form = new WWWForm();
        form.AddField("type", type);
        form.AddField("room", room);

        UnityWebRequest www = UnityWebRequest.Post(Server.mainServer + "/school-management-system/unity/number_players.php", form);
        yield return www.SendWebRequest();
        switch(www.result) {
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
                if(room == "A")
                    Server.playersRoomA = Convert.ToInt32(www.downloadHandler.text);
                else
                    Server.playersRoomB = Convert.ToInt32(www.downloadHandler.text);
                Debug.Log(www.downloadHandler.text);
                break;
            default:
                break;
        }
    }
}