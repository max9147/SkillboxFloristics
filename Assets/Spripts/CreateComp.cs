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
    public TextMeshProUGUI textLast; //Текст показывающий информацию о последнем букете
    public Button buttonCreate;
    public Button buttonReset;
    public Button buttonBack;

    private GameObject[] toDestroy;
    private int countFlowers = 0; //Число цветков в последнем букете

    public void PressCreate() //Нажатие на кнопку создать букет
    {
        buttonCreate.interactable = false; //Запрещаем нажимать на создание букета после создания
        buttonReset.interactable = false;
        buttonBack.interactable = false;
        if (bouquet.activeInHierarchy)
        {
            bouquet.GetComponent<ArrangeBouquet>().ClearBouquet();
        }
        if (basket.activeInHierarchy)
        {

        }
        toDestroy = GameObject.FindGameObjectsWithTag("Flower");
        foreach (var item in toDestroy) //Перебираем цветы на сцене
        {
            countFlowers++;
            Destroy(item);
        }
        textLast.text = $"Цветков: {countFlowers}";
        countFlowers = 0;
        GetComponent<SelectType>().InitSelection();
    }

    public void PressReset()
    {
        buttonCreate.interactable = false;
        buttonReset.interactable = false;
        buttonBack.interactable = false;
        if (bouquet.activeInHierarchy)
        {
            bouquet.GetComponent<ArrangeBouquet>().ClearBouquet();
        }
        if (basket.activeInHierarchy)
        {

        }
        toDestroy = GameObject.FindGameObjectsWithTag("Flower");
        foreach (var item in toDestroy) //Перебираем цветы на сцене
        {
            countFlowers++;
            Destroy(item);
        }
        foreach (var item in flowerSpawers)
        {
            item.GetComponent<DragFlowers>().AllowTaking();
        }
    }
}