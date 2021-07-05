using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TabsChange : MonoBehaviour
{
    public GameObject description;
    public GameObject content;
    public GameObject soundButton;
    public GameObject focusFlowers;
    public GameObject baseFlowers;
    public GameObject fillFlowers;
    public GameObject detailFlowers;
    public GameObject green;
    public TextMeshProUGUI descriptionText;

    public void ChangeTab(int id)
    {
        content.GetComponent<AudioSource>().Play();
        content.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        switch (id)
        {
            case 1:
                focusFlowers.SetActive(true);
                baseFlowers.SetActive(false);
                fillFlowers.SetActive(false);
                detailFlowers.SetActive(false);
                green.SetActive(false);
                content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 900);
                descriptionText.text = "Фокусные цветы - это главные растения в подборе, как правило, это необычные элитарные растения или растения доминирующие " +
                    "за счет большого размера или яркого окраса.";
                break;

            case 2:
                focusFlowers.SetActive(false);
                baseFlowers.SetActive(true);
                fillFlowers.SetActive(false);
                detailFlowers.SetActive(false);
                green.SetActive(false);
                content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 900);
                descriptionText.text = "Базовые цветы - их задача состоит в подчеркивании красоты фокусного цветка, в создании объема букета. Их цена обычно ниже, чем у фокусных цветов. " +
                    "Одно и то же растение может выступать в букете как фокусное растение, так и как базовое, в зависимости от общего ассортимента. " +
                    "В составе базовых растений лучше замешивать не меньше 3 - х видов или сортов растений с разным размеров головы, отличающиеся по внешнему строению.";
                break;

            case 3:
                focusFlowers.SetActive(false);
                baseFlowers.SetActive(false);
                fillFlowers.SetActive(true);
                detailFlowers.SetActive(false);
                green.SetActive(false);
                content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 400);
                descriptionText.text = "Растения с мелким бутоном или с цветками небольшого размера, собранными в соцветия. Они заполняют пространство " +
                    "между крупными бутонами, добавляют в букет разноразмерности.";
                break;

            case 4:
                focusFlowers.SetActive(false);
                baseFlowers.SetActive(false);
                fillFlowers.SetActive(false);
                detailFlowers.SetActive(true);
                green.SetActive(false);
                content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 650);
                descriptionText.text = "Как правило, это растения с небольшим размером бутона, не всегда добавляющие подбору объема, но при этом их необычная легкая структура усиливает " +
                    "выразительные качества состава. Обычно их располагают над общим уровнем цветов и часто называют “бабочками”.";
                break;

            case 5:
                focusFlowers.SetActive(false);
                baseFlowers.SetActive(false);
                fillFlowers.SetActive(false);
                detailFlowers.SetActive(false);
                green.SetActive(true);
                content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 780);
                descriptionText.text = "Зелень в букете может выполнять техническую роль (фиксация стеблей, создание зеленой подушки) или декоративную (интересный окрас " +
                    "или форма листьев, ветвей). Для создания выигрышного подбора рекомендуем сочетать несколько видов зелени, отличающейся по размеру, форме или окрасу листа.";
                break;

            default:
                break;
        }
    }

    public void OpenDescription()
    {
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