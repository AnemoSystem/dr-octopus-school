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
    public Button[] buttons;
    public GameObject loadingIndicator;

    // Global Variables
    public static string username = "username";
    public static int playersRoomA = 0;
    public static int playersRoomB = 0;
    public static bool canMove = true;
    public static int bonusCoins = 0;
    public static string actualMinigame = "";
    public static string mainServer = "https://anemostudy.x10.mx";
    //public static string mainServer = "http://localhost/school-management-system/";
    public static bool firstOpenning = false;
    public static string idServer = "";
    public static bool canStartDialogue = false;

    public void Login() {
        StartCoroutine(Upload());
        EnableAllButtons(false);
        loadingIndicator.SetActive(true);
    }

    void EnableAllButtons(bool state) {
        foreach(Button b in buttons) {
            b.interactable = state;
        }
    }

    IEnumerator Upload() {
        bool updateEntries = false;
        WWWForm form = new WWWForm();
        form.AddField("username", usernameField.text);
        form.AddField("password", passwordField.text);
        
        //UnityWebRequest www = UnityWebRequest.Post("https://revisory-claws.000webhostapp.com/unity/login.php", form);
        UnityWebRequest www = UnityWebRequest.Post(mainServer + "/unity/login.php", form);
        www.certificateHandler = new BypassCertificate();
        yield return www.SendWebRequest();
        
        switch (www.result)
        {
            case UnityWebRequest.Result.ConnectionError:
                //Debug.Log("Connection Error");
                errorWindow.SetActive(true);
                errorDisplay.text = "Erro de conexão!";
                errorDisplay.color = Color.red;
                EnableAllButtons(true);
                break;
            case UnityWebRequest.Result.DataProcessingError:
                //Debug.Log("Data Processing Error");
                errorWindow.SetActive(true);
                errorDisplay.text = "Erro de processamento de dados!";
                errorDisplay.color = Color.yellow;
                EnableAllButtons(true);
                break;
            case UnityWebRequest.Result.ProtocolError:
                //Debug.Log("HTTP Error");
                errorWindow.SetActive(true);
                errorDisplay.text = "Erro de HTTP!";
                errorDisplay.color = Color.magenta;
                EnableAllButtons(true);
                break;
            case UnityWebRequest.Result.Success:
                //Debug.Log("Form upload complete!");
                //Debug.Log(www.downloadHandler.text); 
                username = usernameField.text; 
                if(www.downloadHandler.text == "Login Success - Disconnected") {   
                    //SceneManager.LoadScene("Lobby");
                    updateEntries = true;
                }
                else if(www.downloadHandler.text == "Login Success - Connected") {
                    errorWindow.SetActive(true);
                    errorDisplay.text = "Usuário conectado em outro dispositivo. Disconecte-se nele para entrar.";                    
                    EnableAllButtons(true);
                }
                else {
                    errorWindow.SetActive(true);
                    errorDisplay.text = "Usuário ou senha inexistentes!";
                    EnableAllButtons(true);
                }
                break;
            default:
                errorDisplay.text = "Não foi possível logar devido ao tempo de resposta.";
                errorDisplay.color = Color.red;
                break;
        }
        loadingIndicator.SetActive(false);

        if(updateEntries) {
            www = UnityWebRequest.Post(mainServer + "/unity/update_entries.php", form);
            www.certificateHandler = new BypassCertificate();
            yield return www.SendWebRequest();
            switch(www.result)
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
                    string result = www.downloadHandler.text;
                    if(result == "1") firstOpenning = true;
                    transition.FadeIn("Lobby");
                    break;
                default:
                    break;
            }
        }
        www.Dispose();
    }

    public void CloseWindowError() {
        errorWindow.SetActive(false);
    }
}