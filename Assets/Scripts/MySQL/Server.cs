using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Server : MonoBehaviour
{
    public InputField usernameField;
    public InputField passwordField;

    // Global Variable
    public static string username;

    public void Login() {
        StartCoroutine(Upload());
    }

    IEnumerator Upload() {
        WWWForm form = new WWWForm();
        form.AddField("username", usernameField.text);
        form.AddField("password", passwordField.text);
        
        UnityWebRequest www = UnityWebRequest.Post("http://localhost/school-system/unity/login.php", form);
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
                username = usernameField.text;
                if(www.downloadHandler.text == "Login Success - Disconnected")    
                    SceneManager.LoadScene("NewLobby");
                break;
        }
        www.Dispose();
    }
}