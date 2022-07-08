using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;

public class StudentData : MonoBehaviour
{
    public Text teacherLabel;
    public Text subjectLabel;
    public Text[] gradesLabels;

    public void SearchGrades() {
        StartCoroutine(GetGrades());
    }

    IEnumerator GetGrades() {
        WWWForm form = new WWWForm();
        form.AddField("username", Server.username);
        form.AddField("subject", "Matemática");

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/school-management-system/unity/grades.php", form);
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
                Debug.Log(www.downloadHandler.text);
                string result = www.downloadHandler.text.ToString();
                
                string[] row = result.Split('#');
                List<string> r = new List<string>(row);
                r.RemoveAt(row.Length - 1);
                row = r.ToArray();
                
                Debug.Log("Length Row: " + row.Length);
                string[] data_values;
                int month;
                for(int i = 0; i < row.Length; i++) {
                    data_values = row[i].Split('-');
                    teacherLabel.text = "Professor: " + data_values[0];
                    subjectLabel.text = "Matéria: " + data_values[1];
                    month = Convert.ToInt32(data_values[3]);
                    gradesLabels[month - 1].text = data_values[3] + "º Bimestre: " + data_values[2];
                }
                break;
            default:
                //errorDisplay.text = "Não foi possível logar devido ao tempo de resposta.";
                //errorDisplay.color = Color.red;
                break;
        }
        www.Dispose();
    }
}
