using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UpdateBodyPart : MonoBehaviour
{
    public CustomBodyPartMenu cs;

    void Start() {
        Server.bonusCoins = 0;
    }

    public void StartUpdateDatabase() {
        StartCoroutine(UpdateDatabaseParts());
    }

    IEnumerator UpdateDatabaseParts() {
        WWWForm form = new WWWForm();
        form.AddField("username", Server.username);
        form.AddField("id_skin", cs.playerBodyPart.idSkin);
        form.AddField("id_hair", cs.playerBodyPart.idHair);
        form.AddField("id_torso", cs.playerBodyPart.idTorso);
        form.AddField("id_legs", cs.playerBodyPart.idLegs);

        //UnityWebRequest www = UnityWebRequest.Post("https://revisory-claws.000webhostapp.com/unity/send_data.php", form);
        UnityWebRequest www = UnityWebRequest.Post(Server.mainServer + "/unity/send_data.php", form);
        www.certificateHandler = new BypassCertificate();
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
