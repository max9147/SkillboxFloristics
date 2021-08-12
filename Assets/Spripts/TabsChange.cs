using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TabsChange : MonoBehaviour
{
    public GameObject bouquet;
    public GameObject basket;
    public GameObject description;
    public GameObject content;
    public GameObject log;
    public GameObject focusFlowers;
    public GameObject baseFlowers;
    public GameObject fillFlowers;
    public GameObject detailFlowers;
    public GameObject green;
    public GameObject[] buttons;
    public Sprite button;
    public Sprite buttonGreen;
    public Sprite buttonPressed;
    public Sprite buttonGreenPressed;
    public TextMeshProUGUI descriptionText;

    private GameObject toDestroy;

    public void ChangeTab(int id)
    {
        toDestroy = GameObject.FindGameObjectWithTag("Description");
        Destroy(toDestroy);
        content.GetComponent<AudioSource>().Play();
        content.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);

        foreach (var item in buttons)
            item.GetComponent<Image>().sprite = button;
        buttons[5].GetComponent<Image>().sprite = buttonGreen;
        if (id == 5) buttons[5].GetComponent<Image>().sprite = buttonGreenPressed;
        else buttons[id].GetComponent<Image>().sprite = buttonPressed;

        switch (id)
        {
            case 0:
                log.SetActive(true);
                focusFlowers.SetActive(false);
                baseFlowers.SetActive(false);
                fillFlowers.SetActive(false);
                detailFlowers.SetActive(false);
                green.SetActive(false);
                if (bouquet.activeSelf) content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() * 100);
                else content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, basket.GetComponent<ArrangeBasket>().GetFlowerCount() * 100);
                descriptionText.text = "Во вкладке \"Ваш букет\" можно смотреть и редактировать цветы, участвующие в композиции.";
                break;
            case 1:
                log.SetActive(false);
                focusFlowers.SetActive(true);
                baseFlowers.SetActive(false);
                fillFlowers.SetActive(false);
                detailFlowers.SetActive(false);
                green.SetActive(false);
                content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 1570);
                descriptionText.text = "Фокусные цветы - это главные растения в подборе, как правило, это необычные элитарные растения или растения доминирующие " +
                    "за счет большого размера или яркого окраса.";
                break;

            case 2:
                log.SetActive(false);
                focusFlowers.SetActive(false);
                baseFlowers.SetActive(true);
                fillFlowers.SetActive(false);
                detailFlowers.SetActive(false);
                green.SetActive(false);
                content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 3130);
                descriptionText.text = "Базовые цветы - их задача состоит в подчеркивании красоты фокусного цветка, в создании объема букета. Их цена обычно ниже, чем у фокусных цветов. " +
                    "Одно и то же растение может выступать в букете как фокусное растение, так и как базовое, в зависимости от общего ассортимента. " +
                    "В составе базовых растений лучше замешивать не меньше 3 - х видов или сортов растений с разным размеров головы, отличающиеся по внешнему строению.";
                break;

            case 3:
                log.SetActive(false);
                focusFlowers.SetActive(false);
                baseFlowers.SetActive(false);
                fillFlowers.SetActive(true);
                detailFlowers.SetActive(false);
                green.SetActive(false);
                content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 1050);
                descriptionText.text = "Растения с мелким бутоном или с цветками небольшого размера, собранными в соцветия. Они заполняют пространство " +
                    "между крупными бутонами, добавляют в букет разноразмерности.";
                break;

            case 4:
                log.SetActive(false);
                focusFlowers.SetActive(false);
                baseFlowers.SetActive(false);
                fillFlowers.SetActive(false);
                detailFlowers.SetActive(true);
                green.SetActive(false);
                content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 660);
                descriptionText.text = "Как правило, это растения с небольшим размером бутона, не всегда добавляющие подбору объема, но при этом их необычная легкая структура усиливает " +
                    "выразительные качества состава. Обычно их располагают над общим уровнем цветов и часто называют “бабочками”.";
                break;

            case 5:
                log.SetActive(false);
                focusFlowers.SetActive(false);
                baseFlowers.SetActive(false);
                fillFlowers.SetActive(false);
                detailFlowers.SetActive(false);
                green.SetActive(true);
                content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 790);
                descriptionText.text = "Зелень в букете может выполнять техническую роль (фиксация стеблей, создание зеленой подушки) или декоративную (интересный окрас " +
                    "или форма листьев, ветвей). Для создания выигрышного подбора рекомендуем сочетать несколько видов зелени, отличающейся по размеру, форме или окрасу листа.";
                break;

            default:
                break;
        }
    }

    public void OpenDescription()
    {
        toDestroy = GameObject.FindGameObjectWithTag("Description");
        Destroy(toDestroy);
        content.GetComponent<AudioSource>().Play();
        if (description.activeInHierarchy) description.SetActive(false);
        else description.SetActive(true);
    }

    public bool CheckDescriptionOpen()
    {
        if (description.activeInHierarchy) return true;
        else return false;
    }
}