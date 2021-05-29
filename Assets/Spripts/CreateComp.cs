using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CreateComp : MonoBehaviour
{
    public GameObject bouquet;
    public GameObject basket;
    public GameObject[] flowerSpawers;
    public Button buttonCreate;
    public Button buttonReset;
    public Button buttonBack;

    private GameObject[] toDestroy;

    public void PressCreate() //Нажатие на кнопку создать букет
    {
        buttonCreate.GetComponent<AudioSource>().Play();
        buttonCreate.interactable = false; //Запрещаем нажимать на создание букета после создания
        buttonReset.interactable = false;
        buttonBack.interactable = false;
        GetComponent<ScoringSystem>().CountScore();
        if (bouquet.activeInHierarchy)
        {
            bouquet.GetComponent<ArrangeBouquet>().ClearBouquet();
        }
        if (basket.activeInHierarchy)
        {
            basket.GetComponent<ArrangeBasket>().ClearBasket();
        }
    }

    public void PressReset()
    {
        buttonReset.GetComponent<AudioSource>().Play();
        buttonCreate.interactable = false;
        buttonReset.interactable = false;
        buttonBack.interactable = false;
        if (bouquet.activeInHierarchy)
        {
            bouquet.GetComponent<ArrangeBouquet>().ClearBouquet();
        }
        if (basket.activeInHierarchy)
        {
            basket.GetComponent<ArrangeBasket>().ClearBasket();
        }
        GetComponent<ScoringSystem>().addedFlowers.Clear();
        GetComponent<ScoringSystem>().flowerColors.Clear();
        toDestroy = GameObject.FindGameObjectsWithTag("Flower");
        foreach (var item in toDestroy) //Перебираем цветы на сцене
        {
            Destroy(item);
        }
        foreach (var item in flowerSpawers)
        {
            item.GetComponent<DragFlowers>().AllowTaking();
        }
    }

    public void PressRemove()
    {
        buttonBack.GetComponent<AudioSource>().Play();
        GetComponent<ScoringSystem>().RemoveLast();
        toDestroy = GameObject.FindGameObjectsWithTag("Flower");
        if (bouquet.activeInHierarchy)
        {
            foreach (var item in toDestroy) //Перебираем цветы на сцене
            {
                if (item.GetComponent<SpriteRenderer>().sortingOrder == bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount()) //Находим цветы
                {
                    Destroy(item);
                    bouquet.GetComponent<ArrangeBouquet>().RemoveLast();
                }

                if (bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() == 0)
                {
                    buttonReset.interactable = false;
                    buttonBack.interactable = false;
                }

                if (bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() < 20 || bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() > 35)
                {
                    buttonCreate.interactable = false;
                }
            }
        }
        if (basket.activeInHierarchy)
        {
            foreach (var item in toDestroy) //Перебираем цветы на сцене
            {
                if (item.GetComponent<SpriteRenderer>().sortingOrder == basket.GetComponent<ArrangeBasket>().GetFlowerCount()) //Находим цветы
                {
                    Destroy(item);
                    basket.GetComponent<ArrangeBasket>().RemoveLast();
                }

                if (basket.GetComponent<ArrangeBasket>().GetFlowerCount() == 0)
                {
                    buttonReset.interactable = false;
                    buttonBack.interactable = false;
                }

                if (basket.GetComponent<ArrangeBasket>().GetFlowerCount() < 20 || basket.GetComponent<ArrangeBasket>().GetFlowerCount() > 35)
                {
                    buttonCreate.interactable = false;
                }
            }
        }
        if ((bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() >= 20 && bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() <= 35) || (basket.GetComponent<ArrangeBasket>().GetFlowerCount() >= 25 && basket.GetComponent<ArrangeBasket>().GetFlowerCount() <= 35))
        {
            buttonCreate.interactable = true;
        }
    }
}