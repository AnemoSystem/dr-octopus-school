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
    public Text numAbsentLabel;
    public Text numClassLabel;

    private int id = 0; 
    public string[] subjects;

    public void SearchGrades(string subject) {
        StartCoroutine(GetGrades(subject));
    }

    IEnumerator GetGrades(string subject) {
        WWWForm form = new WWWForm();
        form.AddField("username", Server.username);
        form.AddField("subject", subject);

        UnityWebRequest www = UnityWebRequest.Post(Server.mainServer + "/unity/grades.php", form);
        www.certificateHandler = new BypassCertificate();
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
                //Debug.Log(www.downloadHandler.text);
                string result = www.downloadHandler.text.ToString();
                
                string[] row = result.Split('#');
                List<string> r = new List<string>(row);
                r.RemoveAt(row.Length - 1);
                row = r.ToArray();
                
                //Debug.Log("Length Row: " + row.Length);
                string[] data_values;
                int month;
                for(int i = 1; i <= 4; i++) {
                    if(i <= row.Length) {
                        data_values = row[i - 1].Split('%');
                        teacherLabel.text = "Professor: " + data_values[0];
                        subjectLabel.text = "Matéria: " + data_values[1];
                        month = Convert.ToInt32(data_values[3]);
                        if(data_values[2] == "-1.0")
                            gradesLabels[month - 1].text = data_values[3] + "º Bimestre: Nota não definida.";
                        else
                            gradesLabels[month - 1].text = data_values[3] + "º Bimestre: " + data_values[2];
                        numClassLabel.text = "Aulas: " + data_values[4];
                        numAbsentLabel.text = "Faltas: " + data_values[5];
                    }
                    else {
                        gradesLabels[i - 1].text = i.ToString() + "º Bimestre: Nota não definida.";
                    }
                }
                break;
            default:
                //errorDisplay.text = "Não foi possível logar devido ao tempo de resposta.";
                //errorDisplay.color = Color.red;
                break;
        }
        www.Dispose();
    }

    public void NextOrPreviousSearch(bool next) {
        if(next) id++;
        else id--;
        
        if(id >= subjects.Length) id = 0;
        else if(id < 0) id = subjects.Length - 1;
        
        StartCoroutine(GetGrades(subjects[id]));
    }

    void OnDisable() {
        id = 0;
    }
}
