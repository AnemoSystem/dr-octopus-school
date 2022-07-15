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

    private string buyID;
    private string buyType;

    void Start() {
        buyID = "999";
        buyType = "X";
        playerCoins = 0;
        Server.username = "jooj";
        StartCoroutine(UpdateCoinsAndInventory(0));
    }

    public void BuyProduct(string id_type) {
        string[] result = id_type.Split('#');
        buyID = result[0];
        buyType = result[1];
        StartCoroutine(FindProductDatabase(buyID, buyType));
    }

    public void GetFocusPrice() {
        //focusPrice = price;
        GameObject button = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
        ProductDisplay pd = button.transform.parent.gameObject.GetComponent<ProductDisplay>();
        focusPrice = pd.product.price;
    }

    IEnumerator FindProductDatabase(string id, string type) {
        WWWForm form = new WWWForm();
        form.AddField("username", Server.username);
        form.AddField("item_id", id);
        form.AddField("type", type);
        form.AddField("test", 0);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/school-management-system/unity/get_itens.php", form);
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
                break;
        }
        www.Dispose();        
    }

    void StartBuy(int price) {
        if(playerCoins >= price) {
            playerCoins -= price;
            StartCoroutine(UpdateCoinsAndInventory(1));
        }
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

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/school-management-system/unity/get_itens.php", form);
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