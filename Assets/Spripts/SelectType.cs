using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectType : MonoBehaviour
{
    public Button buttonCreate;
    public Button buttonBack;
    public Button buttonContinue;
    public GameObject bouquet;
    public GameObject bouquetBackground;
    public GameObject bouquetTip;
    public GameObject basket;
    public GameObject basketBackground;
    public GameObject selectMenu;
    public GameObject chooseBouquetMenu;
    public GameObject chooseBasketMenu;
    public GameObject soundButton;
    public GameObject positionArrow;
    public Sprite[] bouquets;
    public Sprite bouquetBG;
    public Sprite bouquetBGGray;
    public Sprite bouquetBGBig;
    public Sprite bouquetMaskBig;
    public Sprite bouquetMaskGray;
    public Sprite bouquetMask;
    public Sprite[] baskets;
    public Sprite basketBG;
    public Sprite basketBGBig;
    public Sprite basketMask;
    public Sprite basketMaskBig;
    public Slider sizeSlider;
    public TextMeshProUGUI amountText;

    private bool isOpen = false;

    void Start()
    {
        InitSelection();
    }

    public void InitSelection()
    {
        selectMenu.SetActive(true);
        isOpen = true;
        if (bouquet.activeInHierarchy || basket.activeInHierarchy) buttonContinue.gameObject.SetActive(true);
        else buttonContinue.gameObject.SetActive(false);
    }

    public void SelectBouquet()
    {
        soundButton.GetComponent<AudioSource>().Play();
        selectMenu.SetActive(false);
        chooseBouquetMenu.SetActive(true);        
    }

    public void ChooseBouquet(int id)
    {
        soundButton.GetComponent<AudioSource>().Play();
        chooseBouquetMenu.SetActive(false);
        bouquet.GetComponent<SpriteRenderer>().sprite = bouquets[id];
        bouquet.GetComponent<ArrangeBouquet>().bouquetID = id;        

        GetComponent<LogSystem>().ClearLog();
        GameObject[] toDestroy = GameObject.FindGameObjectsWithTag("Flower");
        foreach (var item in toDestroy) //Перебираем цветы на сцене
        {
            Destroy(item);
        }
        sizeSlider.value = 0;
        amountText.text = "";
        positionArrow.transform.eulerAngles = new Vector3(0, 0, 0);
        if (bouquet.activeInHierarchy) bouquet.GetComponent<ArrangeBouquet>().ClearBouquet();
        if (basket.activeInHierarchy) basket.GetComponent<ArrangeBasket>().ClearBasket();
        GetComponent<ScoringSystem>().addedFlowers.Clear();
        GetComponent<ScoringSystem>().flowerColors.Clear();
        buttonCreate.interactable = false;
        buttonBack.interactable = false;
        GetComponent<ScoringSystem>().CheckColorMeter();

        switch (id)
        {
            case 0:
                bouquet.GetComponent<SpriteMask>().sprite = bouquetMaskBig;
                bouquetBackground.GetComponent<SpriteRenderer>().sprite = bouquetBGBig;
                bouquetBackground.transform.localScale = new Vector3(1, 1, 1);
                bouquetTip.transform.localPosition = new Vector3(0.2f, -2.6f, 0);
                break;          
            case 2:
                bouquet.GetComponent<SpriteMask>().sprite = bouquetMaskGray;
                bouquetBackground.GetComponent<SpriteRenderer>().sprite = bouquetBGGray;
                bouquetBackground.transform.localScale = new Vector3(1.54f, 1.54f, 1);
                bouquetTip.transform.localPosition = new Vector3(-0.1f, -2.6f, 0);
                break;
            default:
                bouquet.GetComponent<SpriteMask>().sprite = bouquetMask;
                bouquetBackground.GetComponent<SpriteRenderer>().sprite = bouquetBG;
                bouquetBackground.transform.localScale = new Vector3(1.81f, 1.81f, 1);
                bouquetTip.transform.localPosition = new Vector3(-0.1f, -2.6f, 0);
                break;
        }

        basket.SetActive(false);
        bouquet.SetActive(true);
        isOpen = false;
    }

    public void SelectBasket()
    {
        soundButton.GetComponent<AudioSource>().Play();        
        selectMenu.SetActive(false);
        chooseBasketMenu.SetActive(true);
    }

    public void ChooseBasket(int id)
    {
        soundButton.GetComponent<AudioSource>().Play();
        chooseBasketMenu.SetActive(false);
        basket.GetComponent<SpriteRenderer>().sprite = baskets[id];
        buttonContinue.gameObject.SetActive(true);

        GetComponent<LogSystem>().ClearLog();
        GameObject[] toDestroy = GameObject.FindGameObjectsWithTag("Flower");
        foreach (var item in toDestroy) //Перебираем цветы на сцене
        {
            Destroy(item);
        }
        sizeSlider.value = 0;
        amountText.text = "";
        positionArrow.transform.eulerAngles = new Vector3(0, 0, 0);
        if (bouquet.activeInHierarchy) bouquet.GetComponent<ArrangeBouquet>().ClearBouquet();
        if (basket.activeInHierarchy) basket.GetComponent<ArrangeBasket>().ClearBasket();
        GetComponent<ScoringSystem>().addedFlowers.Clear();
        GetComponent<ScoringSystem>().flowerColors.Clear();
        buttonCreate.interactable = false;
        buttonBack.interactable = false;
        GetComponent<ScoringSystem>().CheckColorMeter();

        switch (id)
        {
            case 0:
                basket.GetComponent<SpriteMask>().sprite = basketMaskBig;
                basketBackground.GetComponent<SpriteRenderer>().sprite = basketBGBig;
                basketBackground.transform.localPosition = new Vector3(0, 1.6f, 0);
                basketBackground.transform.localScale = new Vector3(1, 1, 1);
                break;
            default:
                basket.GetComponent<SpriteMask>().sprite = basketMask;
                basketBackground.GetComponent<SpriteRenderer>().sprite = basketBG;
                basketBackground.transform.localPosition = new Vector3(0.01f, 0.88f, 0);
                basketBackground.transform.localScale = new Vector3(0.83f, 0.83f, 1);
                break;
        }

        bouquet.SetActive(false);
        basket.SetActive(true);
        isOpen = false;
    }

    public void ReturnSelection()
    {
        soundButton.GetComponent<AudioSource>().Play();
        chooseBasketMenu.SetActive(false);
        chooseBouquetMenu.SetActive(false);
        selectMenu.SetActive(true);
    }

    public void RevertReseting()
    {
        soundButton.GetComponent<AudioSource>().Play();
        chooseBasketMenu.SetActive(false);
        chooseBouquetMenu.SetActive(false);
        selectMenu.SetActive(false);
        isOpen = false;
    }

    public bool CheckIsOpen()
    {
        return isOpen;
    }
}
