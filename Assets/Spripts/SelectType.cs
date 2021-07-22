using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectType : MonoBehaviour
{
    public GameObject bouquet;
    public GameObject bouquetTip;
    public GameObject basket;
    public GameObject basketBackground;
    public GameObject selectMenu;
    public GameObject chooseBouquetMenu;
    public GameObject chooseBasketMenu;
    public GameObject soundButton;
    public Sprite[] bouquets;
    public Sprite bouquetMaskBig;
    public Sprite bouquetMaskGray;
    public Sprite bouquetMask;
    public Sprite[] baskets;
    public Sprite basketBG;
    public Sprite basketBGBig;
    public Sprite basketMask;
    public Sprite basketMaskBig;

    private bool isOpen = false;

    void Start()
    {
        InitSelection();
    }

    public void InitSelection()
    {
        bouquet.SetActive(false);
        basket.SetActive(false);
        selectMenu.SetActive(true);
        isOpen = true;
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

        switch (id)
        {
            case 0:
                bouquet.GetComponent<SpriteMask>().sprite = bouquetMaskBig;
                bouquetTip.transform.localPosition = new Vector3(0.2f, -2.6f, 0);
                break;          
            case 2:
                bouquet.GetComponent<SpriteMask>().sprite = bouquetMaskGray;
                bouquetTip.transform.localPosition = new Vector3(-0.1f, -2.6f, 0);
                break;
            default:
                bouquet.GetComponent<SpriteMask>().sprite = bouquetMask;
                bouquetTip.transform.localPosition = new Vector3(-0.1f, -2.6f, 0);
                break;
        }

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

        basket.SetActive(true);
        isOpen = false;
    }

    public bool CheckIsOpen()
    {
        return isOpen;
    }
}
