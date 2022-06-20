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
    public Text errorDisplay;

    // Global Variable
    public static string username;

    public void Login() {
        StartCoroutine(Upload());
    }

    IEnumerator Upload() {
        WWWForm form = new WWWForm();
        form.AddField("username", usernameField.text);
        form.AddField("password", passwordField.text);
        
        UnityWebRequest www = UnityWebRequest.Post("https://revisory-claws.000webhostapp.com/unity/login.php", form);
        yield return www.SendWebRequest();
        
        switch (www.result)
        {
            case UnityWebRequest.Result.ConnectionError: /*transformar os debugs dos cases em labels de cores diferentes dentro do unity*/
                Debug.Log("Connection Error");
                errorDisplay.text = "Erro de conexão!";
                errorDisplay.color = Color.red;
                yield return new WaitForSeconds(5);
                errorDisplay.text = " ";
                break;
            case UnityWebRequest.Result.DataProcessingError:
                Debug.Log("Data Processing Error");
                errorDisplay.text = "Erro de processamento de dados!";
                errorDisplay.color = Color.yellow;
                yield return new WaitForSeconds(5);
                errorDisplay.text = " ";
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.Log("HTTP Error");
                errorDisplay.text = "Erro de HTTP!";
                errorDisplay.color = Color.magenta;
                yield return new WaitForSeconds(5);
                errorDisplay.text = " ";
                break;
            case UnityWebRequest.Result.Success: //terminando os cases acima, avisar para o mr gui pra ele mostrar como faz o case desta linha
               // Debug.Log("Form upload complete!");
               // Debug.Log(www.downloadHandler.text); 
                username = usernameField.text; 
                if(www.downloadHandler.text == "Login Success - Disconnected")    
                    SceneManager.LoadScene("Lobby");
                break;
        }
        www.Dispose();
    }
}