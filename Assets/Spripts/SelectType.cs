using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectType : MonoBehaviour
{
    public GameObject bouquet;
    public GameObject basket;
    public GameObject selectMenu;
    public GameObject soundButton;

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
        bouquet.SetActive(true);
        selectMenu.SetActive(false);
        isOpen = false;
    }

    public void SelectBasket()
    {
        soundButton.GetComponent<AudioSource>().Play();
        basket.SetActive(true);
        selectMenu.SetActive(false);
        isOpen = false;
    }

    public bool CheckIsOpen()
    {
        return isOpen;
    }
}
