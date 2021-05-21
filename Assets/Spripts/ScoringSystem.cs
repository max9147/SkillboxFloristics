using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoringSystem : MonoBehaviour
{
    public GameObject scoreScreen;
    public GameObject bouquet;
    public GameObject basket;
    public GameObject soundButton;
    public List<GameObject> addedFlowers = new List<GameObject>();
    public TextMeshProUGUI flowerCountText;
    public TextMeshProUGUI focusCountText;
    public TextMeshProUGUI baseCountText;
    public TextMeshProUGUI fillCountText;
    public TextMeshProUGUI detailsCountText;
    public TextMeshProUGUI greenCountText;
    public TextMeshProUGUI totalScoreText;
    public TextMeshProUGUI focusScoreText;
    public TextMeshProUGUI baseScoreText;
    public TextMeshProUGUI fillScoreText;
    public TextMeshProUGUI detailsScoreText;
    public TextMeshProUGUI greenScoreText;

    private GameObject[] toDestroy;
    private int focusCount = 0;
    private int baseCount = 0;
    private int fillCount = 0;
    private int detailsCount = 0;
    private int greenCount = 0;
    private float totalScore = 0;
    private float focusScore = 0;
    private float baseScore = 0;
    private float fillScore = 0;
    private float detailsScore = 0;
    private float greenScore = 0;
    private bool isOpen = false;

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

        if (((float)focusCount / addedFlowers.Count) > 0.11f && ((float)focusCount / addedFlowers.Count) < 0.182f)
        {
            focusScore = 112 - Mathf.Ceil(Mathf.Abs(0.146f - ((float)focusCount / addedFlowers.Count)) * 690);
            if (focusScore > 100) focusScore = 100;
        }
        else
        {
            if (Mathf.Abs(0.146f - ((float)focusCount / addedFlowers.Count)) * 690 > 100)
                focusScore = 0;
            else
                focusScore = 100 - Mathf.Ceil(Mathf.Abs(0.146f - ((float)focusCount / addedFlowers.Count)) * 690);
        }


        if (((float)baseCount / addedFlowers.Count) > 0.312f && ((float)baseCount / addedFlowers.Count) < 0.458f)
        {
            baseScore = 110 - Mathf.Ceil(Mathf.Abs(0.385f - ((float)baseCount / addedFlowers.Count)) * 270);
            if (baseScore > 100) baseScore = 100;
        }
        else
        {
            if (Mathf.Abs(0.385f - ((float)baseCount / addedFlowers.Count)) * 270 > 100)
                baseScore = 0;
            else
                baseScore = 100 - Mathf.Ceil(Mathf.Abs(0.385f - ((float)baseCount / addedFlowers.Count)) * 270);
        }

        if (((float)fillCount / addedFlowers.Count) > 0.171f && ((float)fillCount / addedFlowers.Count) < 0.301f)
        {
            fillScore = 113 - Mathf.Ceil(Mathf.Abs(0.236f - ((float)fillCount / addedFlowers.Count)) * 430);
            if (fillScore > 100) fillScore = 100;
        }
        else
        {
            if (Mathf.Abs(0.236f - ((float)fillCount / addedFlowers.Count)) * 430 > 100)
                fillScore = 0;
            else
                fillScore = 100 - Mathf.Ceil(Mathf.Abs(0.236f - ((float)fillCount / addedFlowers.Count)) * 430);
        }

        if (((float)detailsCount / addedFlowers.Count) > 0.173f && ((float)detailsCount / addedFlowers.Count) < 0.251f)
        {
            detailsScore = 113 - Mathf.Ceil(Mathf.Abs(0.212f - ((float)detailsCount / addedFlowers.Count)) * 480);
            if (detailsScore > 100) detailsScore = 100;
        }
        else
        {
            if (Mathf.Abs(0.212f - ((float)detailsCount / addedFlowers.Count)) * 480 > 100)
                detailsScore = 0;
            else
                detailsScore = 100 - Mathf.Ceil(Mathf.Abs(0.212f - ((float)detailsCount / addedFlowers.Count)) * 480);
        }

        if (greenCount > 0 && greenCount < 3)
            greenScore = 50;
        else
            greenScore = 0;
        totalScore = focusScore + baseScore + fillScore + detailsScore + greenScore;
    }

    public void CountScore()
    {
        toDestroy = GameObject.FindGameObjectsWithTag("Flower");
        foreach (var item in toDestroy)
            item.GetComponent<SpriteRenderer>().enabled = false;
        bouquet.GetComponent<SpriteRenderer>().enabled = false;
        basket.GetComponent<SpriteRenderer>().enabled = false;
        CheckType();
        scoreScreen.SetActive(true);
        isOpen = true;
        flowerCountText.text = $"Всего цветков: {addedFlowers.Count}";
        focusCountText.text = $"Фокусных: {focusCount}";
        focusScoreText.text = $"{focusScore} очков";
        focusCount = 0;
        baseCountText.text = $"Базовых: {baseCount}";
        baseScoreText.text = $"{baseScore} очков";
        baseCount = 0;
        fillCountText.text = $"Заполняющих: {fillCount}";
        fillScoreText.text = $"{fillScore} очков";
        fillCount = 0;
        detailsCountText.text = $"Деталей: {detailsCount}";
        detailsScoreText.text = $"{detailsScore} очков";
        detailsCount = 0;
        greenCountText.text = $"Зелени: {greenCount}";
        greenScoreText.text = $"{greenScore} очков";
        greenCount = 0;
        totalScoreText.text = $"Итоговый счет: {totalScore}";
    }

    public void CloseScore()
    {
        soundButton.GetComponent<AudioSource>().Play();
        bouquet.GetComponent<SpriteRenderer>().enabled = true;
        basket.GetComponent<SpriteRenderer>().enabled = true;
        foreach (var item in toDestroy) //Перебираем цветы на сцене
            Destroy(item);
        addedFlowers.Clear();
        scoreScreen.SetActive(false);
        isOpen = false;
        GetComponent<SelectType>().InitSelection();
    }

    public void AddFlower(GameObject flower)
    {
        addedFlowers.Add(flower);
    }

    public bool CheckIsOpen()
    {
        return isOpen;
    }
}
