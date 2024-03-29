using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Networking;
using System;
using UnityEngine.SceneManagement;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;

    public float minX;
    public float minY;
    public float maxY;
    public float maxX;

    private string[] data_values = {"0", "0", "0", "0"};

    CustomBodyPart custom;

    public MenuPlayerController menuPlayer;
    public bool changeColor = false;

    void Start() {
        GameObject p = GameObject.Find("Player");

        Vector2 randomPosition = new Vector2(UnityEngine.Random.Range(minX, maxX), UnityEngine.Random.Range(minY, maxY));
        if(p == null && Movimentation.LocalPlayerInstance == null) {
            p = PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
            p.name = Server.username;
            //PhotonPlayer.player = p;
            Reference uname = p.GetComponent<Reference>();
            uname.changeColor = this.changeColor;
            custom = p.transform.GetChild(4).gameObject.GetComponent<CustomBodyPart>();
            StartCoroutine(GetDataFromUser());
        }
    }
    
    IEnumerator GetDataFromUser() {
        WWWForm form = new WWWForm();
        form.AddField("username", Server.username);
        
        //UnityWebRequest www = UnityWebRequest.Post("https://revisory-claws.000webhostapp.com/unity/get_data.php", form);
        UnityWebRequest www = UnityWebRequest.Post(Server.mainServer + "/unity/get_data.php", form);
        www.certificateHandler = new BypassCertificate();
        yield return www.SendWebRequest();
        
        switch (www.result)
        {
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
                data_values = result.Split('-');

                custom.idSkin = Convert.ToInt32(data_values[0]);
                custom.idTorso = Convert.ToInt32(data_values[1]);
                custom.idHair = Convert.ToInt32(data_values[2]);
                custom.idLegs = Convert.ToInt32(data_values[3]);
                break;
        }
    }
}