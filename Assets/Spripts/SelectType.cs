using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectType : MonoBehaviour
{
    public GameObject bouquet;
    public GameObject basket;
    public GameObject selectMenu;

    void Start()
    {
        InitSelection();
    }

    public void InitSelection()
    {
        bouquet.SetActive(false);
        basket.SetActive(false);
        selectMenu.SetActive(true);
    }

    public void SelectBouquet()
    {
        bouquet.SetActive(true);
        selectMenu.SetActive(false);
    }

    public void SelectBasket()
    {
        basket.SetActive(true);
        selectMenu.SetActive(false);
    }
}
