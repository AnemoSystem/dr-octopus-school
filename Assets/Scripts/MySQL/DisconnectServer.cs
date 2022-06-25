using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DisconnectServer : MonoBehaviour
{
    public void CloseGame() {
        StartCoroutine(Disconnect());
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
