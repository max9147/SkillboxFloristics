using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoringSystem : MonoBehaviour
{
    public GameObject scoreScreen;
    public GameObject bouquet;
    public GameObject basket;
    public List<GameObject> addedFlowers = new List<GameObject>();
    public TextMeshProUGUI flowerCountText;
    public TextMeshProUGUI focusCountText;
    public TextMeshProUGUI baseCountText;
    public TextMeshProUGUI fillCountText;
    public TextMeshProUGUI detailsCountText;
    public TextMeshProUGUI greenCountText;

    private GameObject[] toDestroy;
    private int focusCount = 0;
    private int baseCount = 0;
    private int fillCount = 0;
    private int detailsCount = 0;
    private int greenCount = 0;

    private void CheckType()
    {
        foreach (var item in addedFlowers)
        {
            switch (item.GetComponent<SpriteRenderer>().sortingLayerName)
            {
                case "Focus":
                    focusCount++;
                    break;

                case "Base":
                    baseCount++;
                    break;

                case "Fill":
                    fillCount++;
                    break;

                case "Details":
                    detailsCount++;
                    break;

                case "Green":
                    greenCount++;
                    break;

                default:
                    break;
            }
        }
    }

    public void CountScore()
    {
        toDestroy = GameObject.FindGameObjectsWithTag("Flower");
        foreach (var item in toDestroy)
        {
            item.GetComponent<SpriteRenderer>().enabled = false;
        }
        bouquet.GetComponent<SpriteRenderer>().enabled = false;
        basket.GetComponent<SpriteRenderer>().enabled = false;
        CheckType();
        scoreScreen.SetActive(true);
        flowerCountText.text = $"Всего цветков: {addedFlowers.Count}";
        focusCountText.text = $"Фокусных: {focusCount}";
        focusCount = 0;
        baseCountText.text = $"Базовых: {baseCount}";
        baseCount = 0;
        fillCountText.text = $"Заполняющих: {fillCount}";
        fillCount = 0;
        detailsCountText.text = $"Деталей: {detailsCount}";
        detailsCount = 0;
        greenCountText.text = $"Зелени: {greenCount}";
        greenCount = 0;
    }

    public void CloseScore()
    {
        bouquet.GetComponent<SpriteRenderer>().enabled = true;
        basket.GetComponent<SpriteRenderer>().enabled = true;
        foreach (var item in toDestroy) //Перебираем цветы на сцене
        {
            Destroy(item);
        }
        addedFlowers.Clear();
        scoreScreen.SetActive(false);
        GetComponent<SelectType>().InitSelection();
    }

    public void AddFlower(GameObject flower)
    {
        addedFlowers.Add(flower);
    }
}
