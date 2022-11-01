using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopManager : MonoBehaviour
{
    private int playerCoins;
    public Text playerCoinsText;
    private int focusPrice;
    public GameObject window;
    public GameObject windowConfirm;
    public Text windowMessage;
    private string buyID;
    private string buyType;
    public Button[] buttons;
    private string idSelected;

    void OnEnable() {
        CloseWindow();
        CloseWindowConfirm();
    }

    void Start() {
        buyID = "999";
        buyType = "X";
        playerCoins = 0;
        //Server.username = "jooj";
        StartCoroutine(UpdateCoinsAndInventory(0));
    }

    void ActivateButtons(bool state) {
        foreach(Button b in buttons) {
            b.interactable = state;
        }
    }

    void OpenWindow(string message) {
        window.SetActive(true);
        windowMessage.text = message;
        ActivateButtons(false);
    }

    public void CloseWindow() {
        window.SetActive(false);
        ActivateButtons(true);
    }


    public void OpenWindowConfirm(string idType) {
        StartCoroutine(StartOpenWindowConfirm(idType));
    }

    IEnumerator StartOpenWindowConfirm(string idType) {
        yield return StartCoroutine(GetFocusPrice());
        idSelected = idType;
        windowConfirm.SetActive(true);
        ActivateButtons(false);
    }

    public void CloseWindowConfirm() {
        windowConfirm.SetActive(false);
        ActivateButtons(true);        
    }

    public void BuyProduct() {
        string[] result = idSelected.Split('#');
        buyID = result[0];
        buyType = result[1];
        StartCoroutine(FindProductDatabase(buyID, buyType));
    }

    IEnumerator GetFocusPrice() {
        //focusPrice = price;
        GameObject button = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
        ProductDisplay pd = button.transform.parent.gameObject.GetComponent<ProductDisplay>();
        focusPrice = pd.product.price;
        yield return null;
    }

    IEnumerator FindProductDatabase(string id, string type) {
        WWWForm form = new WWWForm();
        form.AddField("username", Server.username);
        form.AddField("item_id", id);
        form.AddField("type", type);
        form.AddField("test", 0);

        UnityWebRequest www = UnityWebRequest.Post(Server.mainServer + "/unity/get_itens.php", form);
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
                string result = www.downloadHandler.text.ToString();
                if(result == "N")
                    StartBuy(focusPrice);
                else
                    OpenWindow("Você já possui este item.");
                break;
        }
        www.Dispose();        
    }

    void StartBuy(int price) {
        if(playerCoins >= price) {
            playerCoins -= price;
            StartCoroutine(UpdateCoinsAndInventory(1));
            OpenWindow("Compra realizada com sucesso!");
        } else OpenWindow("Moedas insuficientes para comprar o item.");
    }

    void RecountPlayerCoins() {
        playerCoinsText.text = "Suas moedas: " + playerCoins.ToString();
    }

    IEnumerator UpdateCoinsAndInventory(int getSet) {
        WWWForm form = new WWWForm();
        form.AddField("username", Server.username);
        form.AddField("getset", getSet);
        form.AddField("item_id", buyID);
        form.AddField("type", buyType);
        form.AddField("coins", playerCoins);

        UnityWebRequest www = UnityWebRequest.Post(Server.mainServer + "/unity/get_itens.php", form);
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
                string result = www.downloadHandler.text.ToString();
                Debug.Log(result);
                playerCoins = int.Parse(result);
                RecountPlayerCoins();
                break;
        }
        www.Dispose();        
    }
}