using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Server : MonoBehaviour
{
    public InputField username;
    public InputField password;

    public void Login() {
        StartCoroutine(Upload());
    }

    IEnumerator Upload() {
        WWWForm form = new WWWForm();
        form.AddField("username", username.text);
        form.AddField("password", password.text);
        
        UnityWebRequest www = UnityWebRequest.Post("http://localhost/school-system/unity/", form);
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
                Debug.Log("Form upload complete!");
                Debug.Log(www.downloadHandler.text);
                if(www.downloadHandler.text == "Login Success")    
                    SceneManager.LoadScene("Lobby");
                break;
        }
        www.Dispose();
    }
}