using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HangmanController : MonoBehaviour
{
    [SerializeField] GameObject wordContainer;
    [SerializeField] GameObject keyboardContainer; //maybe obsolete
    [SerializeField] GameObject letterContainer;
    [SerializeField] GameObject[] hangmanStages;
    [SerializeField] GameObject letterButton; //obsolete, will be removed later
    [SerializeField] TextAsset possibleWord;
    public TMP_InputField letterInput;
    
    private string word;
    private int incorrectGuesses, correctGuesses;

    void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        //reset data back to original state
        incorrectGuesses = 0;
        correctGuesses = 0;
        foreach(Button child in keyboardContainer.GetComponentsInChildren<Button>())
        {
            child.interactable = true;
        }
        foreach(Transform child in keyboardContainer.GetComponentsInChildren<Transform>())
        {
            Destroy(child.gameObject);
        }
        foreach(GameObject stage in hangmanStages)
        {
            stage.SetActive(false);
        }

        //generate new word
        word = generateWord().ToUpper();
        foreach(char letter in word)
        {
            var temp = Instantiate(letterContainer, wordContainer.transform);
        }

    }

    //obsolete
    /*private void CreateButton(int i)
    {
        GameObject temp = Instantiate(letterButton, keyboardContainer.transform);
        temp.GetComponentInChildren<TextMeshProUGUI>().text = ((char)i).ToString();
        temp.GetComponent<Button>().onClick.AddListener(delegate { CheckLetter(((char)i).ToString()); });
    }*/

    private string generateWord()
    {
        string[] wordList = possibleWord.text.Split('\n');
        string line = wordList[Random.Range(0, wordList.Length - 1)];
        return line.Substring(0, line.Length - 1);
    }

    public void InputButton()
    {
        string inputLetter = letterInput.text;
        Debug.Log(inputLetter);
        if(inputLetter == "" || inputLetter == " ")
        {
            return;
        }

        //CheckLetter(inputLetter);
        letterInput.text = "";
    }

    //obsolete
    /*private void CheckLetter(string inputLetter)
    {
        bool letterInWord = false;
        for(int i = 0; i < word.Length; i++)
        {
            if(inputLetter == word[i].ToString())
            {
                letterInWord = true;
                correctGuesses++;
                wordContainer.GetComponentsInChildren<TextMeshProUGUI>()[i].text = inputLetter;
            }
        }
        if(letterInWord == false)
        {
            incorrectGuesses++;
            hangmanStages[incorrectGuesses - 1].SetActive(true);
        }
        CheckOutcome();
    }*/

    private void CheckOutcome()
    {
        if(correctGuesses == word.Length) //win
        {
            for(int i = 0; i < word.Length; i++)
            {
                wordContainer.GetComponentsInChildren<TextMeshProUGUI>()[i].color = Color.green;
            }
            Invoke("InitializeGame", 3f);
        }

        if(incorrectGuesses == hangmanStages.Length) //lose
        {
            for (int i = 0; i < word.Length; i++)
            {
                wordContainer.GetComponentsInChildren<TextMeshProUGUI>()[i].color = Color.red;
                wordContainer.GetComponentsInChildren<TextMeshProUGUI>()[i].text = word[i].ToString();
            }
            Invoke("InitializeGame", 3f);
        }
    }

}
