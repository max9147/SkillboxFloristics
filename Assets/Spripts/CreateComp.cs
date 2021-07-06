using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CreateComp : MonoBehaviour
{
    public GameObject bouquet;
    public GameObject basket;
    public GameObject positionArrow;
    public GameObject[] flowerSpawers;
    public Button buttonCreate;
    public Button buttonReset;
    public Button buttonBack;
    public Slider sizeSlider;

    private GameObject[] toDestroy;

    public void PressCreate() //Нажатие на кнопку создать букет
    {
        buttonCreate.GetComponent<AudioSource>().Play();
        GetComponent<EditFlowers>().StopEdit();
        buttonCreate.interactable = false; //Запрещаем нажимать на создание букета после создания
        buttonReset.interactable = false;
        buttonBack.interactable = false;
        GetComponent<ScoringSystem>().CountScore();
        if (bouquet.activeInHierarchy) bouquet.GetComponent<ArrangeBouquet>().ClearBouquet();
        if (basket.activeInHierarchy) basket.GetComponent<ArrangeBasket>().ClearBasket();
    }

    public void PressReset()
    {
        buttonReset.GetComponent<AudioSource>().Play();
        GetComponent<EditFlowers>().StopEdit();
        buttonCreate.interactable = false;
        buttonReset.interactable = false;
        buttonBack.interactable = false;
        sizeSlider.value = 0;
        positionArrow.transform.eulerAngles = new Vector3(0, 0, 0);
        if (bouquet.activeInHierarchy) bouquet.GetComponent<ArrangeBouquet>().ClearBouquet();
        if (basket.activeInHierarchy) basket.GetComponent<ArrangeBasket>().ClearBasket();
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
        GetComponent<EditFlowers>().StopEdit();
        if ((bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() >= 1 && bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() <= 30) || (basket.GetComponent<ArrangeBasket>().GetFlowerCount() >= 1 && basket.GetComponent<ArrangeBasket>().GetFlowerCount() <= 30))
            sizeSlider.value -= 0.033f;         
        GetComponent<ScoringSystem>().RemoveFromScoring();
        toDestroy = GameObject.FindGameObjectsWithTag("Flower");
        if (bouquet.activeInHierarchy)
        {
            float rotation = bouquet.GetComponent<ArrangeBouquet>().GetRotation();
            if (rotation > 90) positionArrow.transform.eulerAngles = new Vector3(0, 0, 90);
            else if (rotation < -90) positionArrow.transform.eulerAngles = new Vector3(0, 0, -90);
            else positionArrow.transform.eulerAngles = new Vector3(0, 0, rotation);
            foreach (var item in toDestroy) //Перебираем цветы на сцене
            {
                if (item.GetComponent<SpriteRenderer>().sortingOrder == bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount()) //Находим цветы
                {
                    Destroy(item);
                    bouquet.GetComponent<ArrangeBouquet>().RemoveFlower();
                }

                if (bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() == 0)
                {
                    buttonReset.interactable = false;
                    buttonBack.interactable = false;
                }

                if (bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() < 10 || bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() > 30)
                {
                    buttonCreate.interactable = false;
                }
            }
        }
        if (basket.activeInHierarchy)
        {
            float rotation = basket.GetComponent<ArrangeBasket>().GetRotation();
            if (rotation > 90) positionArrow.transform.eulerAngles = new Vector3(0, 0, 90);
            else if (rotation < -90) positionArrow.transform.eulerAngles = new Vector3(0, 0, -90);
            else positionArrow.transform.eulerAngles = new Vector3(0, 0, rotation);
            foreach (var item in toDestroy) //Перебираем цветы на сцене
            {
                if (item.GetComponent<SpriteRenderer>().sortingOrder == basket.GetComponent<ArrangeBasket>().GetFlowerCount()) //Находим цветы
                {
                    Destroy(item);
                    basket.GetComponent<ArrangeBasket>().RemoveFlower();
                }

                if (basket.GetComponent<ArrangeBasket>().GetFlowerCount() == 0)
                {
                    buttonReset.interactable = false;
                    buttonBack.interactable = false;
                }

                if (basket.GetComponent<ArrangeBasket>().GetFlowerCount() < 10 || basket.GetComponent<ArrangeBasket>().GetFlowerCount() > 30)
                {
                    buttonCreate.interactable = false;
                }
            }
        }
        if ((bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() >= 10 && bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() <= 30) || (basket.GetComponent<ArrangeBasket>().GetFlowerCount() >= 10 && basket.GetComponent<ArrangeBasket>().GetFlowerCount() <= 30))
        {
            buttonCreate.interactable = true;
        }
    }
}