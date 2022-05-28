using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DisconnectQuery : MonoBehaviour
{
    void OnApplicationQuit() {
        StartCoroutine(Disconnect());
    }

    IEnumerator Disconnect() {
        WWWForm form = new WWWForm();
        form.AddField("username", Server.username);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/school-system/unity/disconnect_login.php", form);
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
                break;
        }
        www.Dispose();        
    }
}
