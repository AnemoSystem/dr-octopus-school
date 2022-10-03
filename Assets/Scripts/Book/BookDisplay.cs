using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookDisplay : MonoBehaviour
{
    public BookInfo info;

    public Text title;
    public Image leftPage;
    public Image rightPage;
    public Image bookSprite;
    public Button nextButton;
    public Button previousButton;

    private bool isEven = false;
    private int leftCurrentPage = -2;
    private int rightCurrentPage = -1;

    void Start() {
        ResetBook();
    }

    public void HideBook() {
        bookSprite.gameObject.SetActive(false);
        ResetBook();
    }

    public void ShowBook(BookInfo whichInfo) {
        bookSprite.gameObject.SetActive(false);
        info = whichInfo;
        ResetBook();        
    }

    void ResetBook() {
        leftCurrentPage = -2;
        rightCurrentPage = -1;
        title.text = info.title;
        //leftPage.sprite = info.pages[leftCurrentPage];
        //rightPage.sprite = info.pages[rightCurrentPage];
        CloseBook(true);

        // Returns if number of pages is even
        int test = info.pages.Length % 2;

        isEven = test == 0 ? true : false;
    }

    public void NextPage() {
        leftCurrentPage += 2;
        rightCurrentPage += 2;

        Debug.Log("Left: " + leftCurrentPage.ToString());
        Debug.Log("Right: " + rightCurrentPage.ToString());

        if(leftCurrentPage >= info.pages.Length) {
            CloseBook(false);
            return;
        }

        if(isEven) {
            if (rightCurrentPage > info.pages.Length) {
                leftPage.sprite = info.pages[leftCurrentPage];
                leftPage.gameObject.SetActive(true);
                rightPage.gameObject.SetActive(false);
            } else {
                leftPage.sprite = info.pages[leftCurrentPage];
                rightPage.sprite = info.pages[rightCurrentPage];
                leftPage.gameObject.SetActive(true);
                rightPage.gameObject.SetActive(true);                
            }
        } else {
            if (leftCurrentPage >= info.pages.Length) {
                CloseBook(false);
                return;
            } else {
                leftPage.sprite = info.pages[leftCurrentPage];
                rightPage.sprite = info.pages[rightCurrentPage];
                leftPage.gameObject.SetActive(true);
                rightPage.gameObject.SetActive(true);                
            }
        }
        previousButton.interactable = true;
        nextButton.interactable = true;
        bookSprite.sprite = info.openedSprite;
    }

    public void PreviousPage() {
        leftCurrentPage -= 2;
        rightCurrentPage -= 2;
        if(leftCurrentPage < 0) {
            CloseBook(true);
            return;
        }
        leftPage.sprite = info.pages[leftCurrentPage];
        rightPage.sprite = info.pages[rightCurrentPage];
        leftPage.gameObject.SetActive(true);
        rightPage.gameObject.SetActive(true);
        previousButton.interactable = true;
        nextButton.interactable = true;
        bookSprite.sprite = info.openedSprite;              
    }

    void CloseBook(bool front) {
        leftPage.gameObject.SetActive(false);
        rightPage.gameObject.SetActive(false);
        if(front) {
            bookSprite.sprite = info.frontSprite;
            previousButton.interactable = false;
            nextButton.interactable = true;
        }
        else {
            bookSprite.sprite = info.backwardSprite;
            nextButton.interactable = false;
            previousButton.interactable = true;
        }
    }
}
