using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectType : MonoBehaviour
{
    public GameObject bouquet;
    public GameObject basket;
    public GameObject selectMenu;

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
        bouquet.SetActive(true);
        selectMenu.SetActive(false);
        isOpen = false;
    }

    public void SelectBasket()
    {
        basket.SetActive(true);
        selectMenu.SetActive(false);
        isOpen = false;
    }

    public bool CheckIsOpen()
    {
        return isOpen;
    }
}
