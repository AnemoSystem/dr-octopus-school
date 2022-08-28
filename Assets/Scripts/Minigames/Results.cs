using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Results : MonoBehaviour
{
    public LoadWithTransition transition;
    public Text minigameName;
    public Text coins;
    public Text totalCoins;

    void Start() {
        minigameName.text = Server.actualMinigame;
        coins.text = Server.bonusCoins.ToString() + " moedas";
        StartCoroutine(UpdateCoins());
    }

    /*
    public void Continue() {
        transition.Fade
    }
    */
    IEnumerator UpdateCoins() {
        WWWForm form = new WWWForm();
        form.AddField("username", Server.username);
        form.AddField("coins", Server.bonusCoins);
        
        UnityWebRequest www = UnityWebRequest.Post(Server.mainServer +"/school-management-system/unity/update_coins.php", form);
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
                string result = www.downloadHandler.text.ToString();
                Debug.Log(result);
                totalCoins.text = "Suas moedas totais: " + result;
                break;
            default:
                break;
        }
        www.Dispose();
    }
}