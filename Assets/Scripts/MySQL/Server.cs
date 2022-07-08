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
    public GameObject errorWindow;
    public LoadWithTransition transition;

    // Global Variable
    public static string username = "username";
    public static int playersRoomA = 0;
    public static int playersRoomB = 0;

    public void Login() {
        StartCoroutine(Upload());
    }

    IEnumerator Upload() {
        WWWForm form = new WWWForm();
        form.AddField("username", usernameField.text);
        form.AddField("password", passwordField.text);
        
        //UnityWebRequest www = UnityWebRequest.Post("https://revisory-claws.000webhostapp.com/unity/login.php", form);
        UnityWebRequest www = UnityWebRequest.Post("http://localhost/school-management-system/unity/login.php", form);
        yield return www.SendWebRequest();
        
        switch (www.result)
        {
            case UnityWebRequest.Result.ConnectionError: /*transformar os debugs dos cases em labels de cores diferentes dentro do unity*/
                //Debug.Log("Connection Error");
                errorWindow.SetActive(true);
                errorDisplay.text = "Erro de conexão!";
                errorDisplay.color = Color.red;
                break;
            case UnityWebRequest.Result.DataProcessingError:
                //Debug.Log("Data Processing Error");
                errorWindow.SetActive(true);
                errorDisplay.text = "Erro de processamento de dados!";
                errorDisplay.color = Color.yellow;
                break;
            case UnityWebRequest.Result.ProtocolError:
                //Debug.Log("HTTP Error");
                errorWindow.SetActive(true);
                errorDisplay.text = "Erro de HTTP!";
                errorDisplay.color = Color.magenta;
                break;
            case UnityWebRequest.Result.Success: //terminando os cases acima, avisar para o mr gui pra ele mostrar como faz o case desta linha
                //Debug.Log("Form upload complete!");
                //Debug.Log(www.downloadHandler.text); 
                username = usernameField.text; 
                if(www.downloadHandler.text == "Login Success - Disconnected") {   
                    //SceneManager.LoadScene("Lobby");
                    transition.FadeIn("Lobby");
                }
                else if(www.downloadHandler.text == "Login Success - Connected") {
                    errorWindow.SetActive(true);
                    errorDisplay.text = "Usuário conectado em outro dispositivo. Disconecte-se nele para entrar.";                    
                }
                else {
                    errorWindow.SetActive(true);
                    errorDisplay.text = "Usuário ou senha inexistentes!";
                }
                break;
            default:
                errorDisplay.text = "Não foi possível logar devido ao tempo de resposta.";
                errorDisplay.color = Color.red;
                break;
        }
        www.Dispose();
    }

    public void CloseWindowError() {
        errorWindow.SetActive(false);
    }
}