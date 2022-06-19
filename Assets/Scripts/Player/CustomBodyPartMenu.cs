using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomBodyPartMenu : MonoBehaviour
{
    public CustomBodyPart playerBodyPart;
    public CustomBodyPart menuBodyPart;

    void Start() {
        menuBodyPart = transform.GetChild(3).GetComponent<CustomBodyPart>();
    }

    void OnEnable() {
        FindPlayer();
        UpdateMenuBodyParts();
    }

    public void UpdateMenuBodyParts() {
        menuBodyPart.idSkin = playerBodyPart.idSkin;
        menuBodyPart.idTorso = playerBodyPart.idTorso;
        menuBodyPart.idLegs = playerBodyPart.idLegs;
        menuBodyPart.idHair = playerBodyPart.idHair;
    }

    public void UpdatePlayerBodyParts() {
        playerBodyPart.idSkin = menuBodyPart.idSkin;
        playerBodyPart.idTorso = menuBodyPart.idTorso;
        playerBodyPart.idLegs = menuBodyPart.idLegs;
        playerBodyPart.idHair = menuBodyPart.idHair;
        StartCoroutine(UpdateDatabaseParts());
    }

    public void FindPlayer() {
        playerBodyPart = GameObject.Find(Server.username + "/BodyPartController").GetComponent<CustomBodyPart>();
    }

    IEnumerator UpdateDatabaseParts() {
        WWWForm form = new WWWForm();
        form.AddField("username", Server.username);
        form.AddField("id_skin", playerBodyPart.idSkin);
        form.AddField("id_hair", playerBodyPart.idHair);
        form.AddField("id_torso", playerBodyPart.idTorso);
        form.AddField("id_legs", playerBodyPart.idLegs);

        UnityWebRequest www = UnityWebRequest.Post("https://revisory-claws.000webhostapp.com/unity/send_data.php", form);
        yield return www.SendWebRequest();
        
        switch (www.result)
        {
            case UnityWebRequest.Result.ConnectionError:
                Debug.Log("Send Data - Connection Error");
                break;
            case UnityWebRequest.Result.DataProcessingError:
                Debug.Log("Send Data - Data Processing Error");
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.Log("Send Data - HTTP Error");
                break;
            case UnityWebRequest.Result.Success:
                Debug.Log("Sending data of clothes");
                break;
        }
    }
}