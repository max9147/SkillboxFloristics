using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoringSystem : MonoBehaviour
{
    public GameObject scoreScreen;
    public GameObject soundButton;
    public GameObject sizeSlider;
    public GameObject positionArrow;
    public GameObject[] stars;
    public List<GameObject> addedFlowers = new List<GameObject>();
    public List<FlowerColor> flowerColors = new List<FlowerColor>();
    public Sprite starActive;
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
    public TextMeshProUGUI colorScoreText;

    private GameObject[] toDestroy;
    private int focusCount = 0;
    private int baseCount = 0;
    private int fillCount = 0;
    private int detailsCount = 0;
    private int greenCount = 0;
    private int yellowColored = 0;
    private int lightOrangeColored = 0;
    private int orangeColored = 0;
    private int darkOrangeColored = 0;
    private int redColored = 0;
    private int pinkColored = 0;
    private int violetColored = 0;
    private int darkBlueColored = 0;
    private int blueColored = 0;
    private int lightBlueColored = 0;
    private int greenColored = 0;
    private int lightGreenColored = 0;
    private int monochromeCount = 0;
    private int contrastCount = 0;
    private int harmonyCount = 0;
    private int contrastHarmonyCount = 0;
    private int starCount = 0;
    private float totalScore = 0;
    private float focusScore = 0;
    private float baseScore = 0;
    private float fillScore = 0;
    private float detailsScore = 0;
    private float greenScore = 0;
    private bool isOpen = false;
    private string colorString;

    private int CheckType()
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

        if (((float)focusCount / addedFlowers.Count) >= 0.071f && ((float)focusCount / addedFlowers.Count) <= 0.25f)
        {
            focusScore = 125 - Mathf.Ceil(Mathf.Abs(0.161f - ((float)focusCount / addedFlowers.Count)) * 620);
            if (focusScore > 100) focusScore = 100;
        }
        else
        {
            if (Mathf.Abs(0.161f - ((float)focusCount / addedFlowers.Count)) * 620 > 100)
                focusScore = 0;
            else
                focusScore = 100 - Mathf.Ceil(Mathf.Abs(0.161f - ((float)focusCount / addedFlowers.Count)) * 620);
        }


        if (((float)baseCount / addedFlowers.Count) >= 0.285f && ((float)baseCount / addedFlowers.Count) <= 0.6f)
        {
            baseScore = 110 - Mathf.Ceil(Mathf.Abs(0.443f - ((float)baseCount / addedFlowers.Count)) * 225);
            if (baseScore > 100) baseScore = 100;
        }
        else
        {
            if (Mathf.Abs(0.443f - ((float)baseCount / addedFlowers.Count)) * 225 > 100)
                baseScore = 0;
            else
                baseScore = 100 - Mathf.Ceil(Mathf.Abs(0.443f - ((float)baseCount / addedFlowers.Count)) * 225);
        }

        if (((float)fillCount / addedFlowers.Count) >= 0.2f && ((float)fillCount / addedFlowers.Count) <= 0.467f)
        {
            fillScore = 110 - Mathf.Ceil(Mathf.Abs(0.333f - ((float)fillCount / addedFlowers.Count)) * 300);
            if (fillScore > 100) fillScore = 100;
        }
        else
        {
            if (Mathf.Abs(0.333f - ((float)fillCount / addedFlowers.Count)) * 300 > 100)
                fillScore = 0;
            else
                fillScore = 100 - Mathf.Ceil(Mathf.Abs(0.333f - ((float)fillCount / addedFlowers.Count)) * 300);
        }

        if (((float)detailsCount / addedFlowers.Count) >= 0.1f && ((float)detailsCount / addedFlowers.Count) <= 0.334f)
        {
            detailsScore = 117 - Mathf.Ceil(Mathf.Abs(0.217f - ((float)detailsCount / addedFlowers.Count)) * 460);
            if (detailsScore > 100) detailsScore = 100;
        }
        else
        {
            if (Mathf.Abs(0.217f - ((float)detailsCount / addedFlowers.Count)) * 460 > 100)
                detailsScore = 0;
            else
                detailsScore = 100 - Mathf.Ceil(Mathf.Abs(0.217f - ((float)detailsCount / addedFlowers.Count)) * 460);
        }

        if (greenCount > 0 && greenCount < 3)
            greenScore = 100;
        else
            greenScore = 0;

        int typeScore = (int)(focusScore + baseScore + fillScore + detailsScore + greenScore);

        if (focusScore != 0 && baseScore != 0 && fillScore != 0 && detailsScore != 0 && greenScore != 0) starCount++;
        if (typeScore >= 350) starCount++;

        return typeScore;
    }

    private int CheckColor()
    {
        foreach (var item in flowerColors)
        {
            switch (item)
            {
                case FlowerColor.Yellow:
                    yellowColored++;
                    break;
                case FlowerColor.LightOrange:
                    lightOrangeColored++;
                    break;
                case FlowerColor.Orange:
                    orangeColored++;
                    break;
                case FlowerColor.DarkOrange:
                    darkOrangeColored++;
                    break;
                case FlowerColor.Red:
                    redColored++;
                    break;
                case FlowerColor.Pink:
                    pinkColored++;
                    break;
                case FlowerColor.Violet:
                    violetColored++;
                    break;
                case FlowerColor.DarkBlue:
                    darkBlueColored++;
                    break;
                case FlowerColor.Blue:
                    blueColored++;
                    break;
                case FlowerColor.LightBlue:
                    lightBlueColored++;
                    break;
                case FlowerColor.Green:
                    greenColored++;
                    break;
                case FlowerColor.LightGreen:
                    lightGreenColored++;
                    break;
                default:
                    break;
            }
        }

        CheckMonochrome();
        CheckContrast();
        CheckHarmony();
        CheckContrastHarmony();

        int[] combos = { monochromeCount, contrastCount, harmonyCount, contrastHarmonyCount };
        int max = 0;
        int maxCombo = 4;
        for (int i = 0; i < combos.Length; i++)
        {
            if (combos[i] > max)
            {
                max = combos[i];
                maxCombo = i;
            }
        }
        if (max >= 5) starCount++;
        if (max >= 10) starCount++;
        monochromeCount = 0;
        contrastCount = 0;
        harmonyCount = 0;
        contrastHarmonyCount = 0;
        int colorScore = 0;
        switch (maxCombo)
        {
            case 0:
                colorScore = CheckMonochrome();
                colorString = "Монохромная цветовая гамма";
                break;
            case 1:
                colorScore = CheckContrast();
                colorString = "Контрастная цветовая гамма";
                break;
            case 2:
                colorScore = CheckHarmony();
                colorString = "Гармоническая цветовая гамма";
                break;
            case 3:
                colorScore = CheckContrastHarmony();
                colorString = "Контрастная гармоническая цветовая гамма";
                break;
            default:
                break;
        }

        yellowColored = 0;
        lightOrangeColored = 0;
        orangeColored = 0;
        darkOrangeColored = 0;
        redColored = 0;
        pinkColored = 0;
        violetColored = 0;
        darkBlueColored = 0;
        blueColored = 0;
        lightBlueColored = 0;
        greenColored = 0;
        lightGreenColored = 0;

        return colorScore;
    }

    private int CheckMonochrome()
    {
        int score = 1000;
        FlowerColor most = flowerColors.GroupBy(i => i).OrderByDescending(grp => grp.Count())
      .Select(grp => grp.Key).First();
        foreach (var item in flowerColors)
        {
            if (item != most) score -= 35;
            else monochromeCount++;
        }
        return score;
    }

    private int CheckContrast()
    {
        int score = 2000;
        int yellowViolet = 0;
        int lightOrangeDarkBlue = 0;
        int orangeBlue = 0;
        int darkOrangeLightBlue = 0;
        int redGreen = 0;
        int pinkLightGreen = 0;
        foreach (var item in flowerColors)
        {
            switch (item)
            {
                case FlowerColor.Yellow:
                    yellowViolet++;
                    break;
                case FlowerColor.LightOrange:
                    lightOrangeDarkBlue++;
                    break;
                case FlowerColor.Orange:
                    orangeBlue++;
                    break;
                case FlowerColor.DarkOrange:
                    darkOrangeLightBlue++;
                    break;
                case FlowerColor.Red:
                    redGreen++;
                    break;
                case FlowerColor.Pink:
                    pinkLightGreen++;
                    break;
                case FlowerColor.Violet:
                    yellowViolet++;
                    break;
                case FlowerColor.DarkBlue:
                    lightOrangeDarkBlue++;
                    break;
                case FlowerColor.Blue:
                    orangeBlue++;
                    break;
                case FlowerColor.LightBlue:
                    darkOrangeLightBlue++;
                    break;
                case FlowerColor.Green:
                    redGreen++;
                    break;
                case FlowerColor.LightGreen:
                    pinkLightGreen++;
                    break;
                default:
                    break;
            }
        }
        int[] combos = { yellowViolet, lightOrangeDarkBlue, orangeBlue, darkOrangeLightBlue, redGreen, pinkLightGreen };
        int max = 0;
        int maxCombo=6;
        for (int i = 0; i < combos.Length; i++)
        {
            if (combos[i] > max)
            {
                max = combos[i];
                maxCombo = i;
            }
        }
        contrastCount = max;
        switch (maxCombo)
        {
            case 0:
                if (yellowColored == violetColored) score = 1000;
                break;
            case 1:
                if (lightOrangeColored == darkBlueColored) score = 1000;
                break;
            case 2:
                if (orangeColored == blueColored) score = 1000;
                break;
            case 3:
                if (darkOrangeColored == lightBlueColored) score = 1000;
                break;
            case 4:
                if (redColored == greenColored) score = 1000;
                break;
            case 5:
                if (pinkColored == lightGreenColored) score = 1000;
                break;
            default:
                break;
        }
        score -= 70 * (flowerColors.Count - max);
        return score;
    }

    private int CheckHarmony()
    {
        int score = 3000;
        int yellowRedBlue = 0;
        int lightOrangePinkLightBlue = 0;
        int orangeVioletGreen = 0;
        int darkOrangeDarkBlueLightGreen = 0;
        foreach (var item in flowerColors)
        {
            switch (item)
            {
                case FlowerColor.Yellow:
                    yellowRedBlue++;
                    break;
                case FlowerColor.LightOrange:
                    lightOrangePinkLightBlue++;
                    break;
                case FlowerColor.Orange:
                    orangeVioletGreen++;
                    break;
                case FlowerColor.DarkOrange:
                    darkOrangeDarkBlueLightGreen++;
                    break;
                case FlowerColor.Red:
                    yellowRedBlue++;
                    break;
                case FlowerColor.Pink:
                    lightOrangePinkLightBlue++;
                    break;
                case FlowerColor.Violet:
                    orangeVioletGreen++;
                    break;
                case FlowerColor.DarkBlue:
                    darkOrangeDarkBlueLightGreen++;
                    break;
                case FlowerColor.Blue:
                    yellowRedBlue++;
                    break;
                case FlowerColor.LightBlue:
                    lightOrangePinkLightBlue++;
                    break;
                case FlowerColor.Green:
                    orangeVioletGreen++;
                    break;
                case FlowerColor.LightGreen:
                    darkOrangeDarkBlueLightGreen++;
                    break;
                default:
                    break;
            }
        }
        int[] combos = { yellowRedBlue, lightOrangePinkLightBlue, orangeVioletGreen, darkOrangeDarkBlueLightGreen };
        int max = 0;
        int maxCombo = 4;
        for (int i = 0; i < combos.Length; i++)
        {
            if (combos[i] > max)
            {
                max = combos[i];
                maxCombo = i;
            }
        }
        harmonyCount = max;
        switch (maxCombo)
        {
            case 0:
                if ((yellowColored == redColored) && (yellowColored == blueColored)) score = 1500;
                break;
            case 1:
                if ((lightOrangeColored == pinkColored) && (lightOrangeColored == lightBlueColored)) score = 1500;
                break;
            case 2:
                if ((orangeColored == violetColored) && (orangeColored == greenColored)) score = 1500;
                break;
            case 3:
                if ((darkOrangeColored == darkBlueColored) && (darkOrangeColored == lightGreenColored)) score = 1500;
                break;
            default:
                break;
        }
        score -= 105 * (flowerColors.Count - max);
        return score;
    }

    private int CheckContrastHarmony()
    {
        int score = 3000;
        int yellowPinkDarkBlue = 0;
        int lightOrangeVioletBlue = 0;
        int orangeDarkBlueLightBlue = 0;
        int darkOrangeBlueGreen = 0;
        int redLightBlueLightGreen = 0;
        int pinkGreenYellow = 0;
        int violetLightGreenLightOrange = 0;
        int darkBlueYellowOrange = 0;
        int blueLightOrangeDarkOrange = 0;
        int lightBlueOrangeRed = 0;
        int greenDarkOrangePink = 0;
        int lightGreenRedViolet = 0;
        foreach (var item in flowerColors)
        {
            switch (item)
            {
                case FlowerColor.Yellow:
                    yellowPinkDarkBlue++;
                    darkBlueYellowOrange++;
                    pinkGreenYellow++;
                    break;
                case FlowerColor.LightOrange:
                    lightOrangeVioletBlue++;
                    blueLightOrangeDarkOrange++;
                    violetLightGreenLightOrange++;
                    break;
                case FlowerColor.Orange:
                    orangeDarkBlueLightBlue++;
                    darkBlueYellowOrange++;
                    lightBlueOrangeRed++;
                    break;
                case FlowerColor.DarkOrange:
                    darkOrangeBlueGreen++;
                    blueLightOrangeDarkOrange++;
                    greenDarkOrangePink++;
                    break;
                case FlowerColor.Red:
                    redLightBlueLightGreen++;
                    lightBlueOrangeRed++;
                    lightGreenRedViolet++;
                    break;
                case FlowerColor.Pink:
                    pinkGreenYellow++;
                    greenDarkOrangePink++;
                    yellowPinkDarkBlue++;
                    break;
                case FlowerColor.Violet:
                    violetLightGreenLightOrange++;
                    lightGreenRedViolet++;
                    lightOrangeVioletBlue++;
                    break;
                case FlowerColor.DarkBlue:
                    darkBlueYellowOrange++;
                    orangeDarkBlueLightBlue++;
                    yellowPinkDarkBlue++;
                    break;
                case FlowerColor.Blue:
                    blueLightOrangeDarkOrange++;
                    darkOrangeBlueGreen++;
                    lightOrangeVioletBlue++;
                    break;
                case FlowerColor.LightBlue:
                    lightBlueOrangeRed++;
                    orangeDarkBlueLightBlue++;
                    redLightBlueLightGreen++;
                    break;
                case FlowerColor.Green:
                    greenDarkOrangePink++;
                    darkOrangeBlueGreen++;
                    pinkGreenYellow++;
                    break;
                case FlowerColor.LightGreen:
                    lightGreenRedViolet++;
                    redLightBlueLightGreen++;
                    violetLightGreenLightOrange++;
                    break;
                default:
                    break;
            }
        }
        int[] combos = { yellowPinkDarkBlue, lightOrangeVioletBlue, orangeDarkBlueLightBlue, darkOrangeBlueGreen, redLightBlueLightGreen, pinkGreenYellow, violetLightGreenLightOrange,
            darkBlueYellowOrange, blueLightOrangeDarkOrange, lightBlueOrangeRed, greenDarkOrangePink, lightGreenRedViolet };
        int max = 0;
        int maxCombo = 12;
        for (int i = 0; i < combos.Length; i++)
        {
            if (combos[i] > max)
            {
                max = combos[i];
                maxCombo = i;
            }
        }
        contrastHarmonyCount = max;
        switch (maxCombo)
        {
            case 0:
                if ((yellowColored == pinkColored) && (yellowColored == darkBlueColored)) score = 1500;
                break;
            case 1:
                if ((lightOrangeColored == violetColored) && (lightOrangeColored == blueColored)) score = 1500;
                break;
            case 2:
                if ((orangeColored == darkBlueColored) && (orangeColored == lightBlueColored)) score = 1500;
                break;
            case 3:
                if ((darkOrangeColored == blueColored) && (darkOrangeColored == greenColored)) score = 1500;
                break;
            case 4:
                if ((redColored == lightBlueColored) && (redColored == lightGreenColored)) score = 1500;
                break;
            case 5:
                if ((pinkColored == greenColored) && (pinkColored == yellowColored)) score = 1500;
                break;
            case 6:
                if ((violetColored == lightGreenColored) && (violetColored == lightOrangeColored)) score = 1500;
                break;
            case 7:
                if ((darkBlueColored == yellowColored) && (darkBlueColored == orangeColored)) score = 1500;
                break;
            case 8:
                if ((blueColored == lightOrangeColored) && (blueColored == darkOrangeColored)) score = 1500;
                break;
            case 9:
                if ((lightBlueColored == orangeColored) && (lightBlueColored == redColored)) score = 1500;
                break;
            case 10:
                if ((greenColored == darkOrangeColored) && (greenColored == pinkColored)) score = 1500;
                break;
            case 11:
                if ((lightGreenColored == redColored) && (lightGreenColored == violetColored)) score = 1500;
                break;
            default:
                break;
        }
        score -= 105 * (flowerColors.Count - max);
        return score;
    }

    public void CountScore()
    {
        totalScore += CheckType();
        totalScore += CheckColor();
        scoreScreen.SetActive(true);
        isOpen = true;
        for (int i = 0; i < starCount; i++) stars[i].GetComponent<Image>().sprite = starActive;
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
        colorScoreText.text = $"{colorString}: {CheckColor()} очков";
        totalScoreText.text = $"Итоговый счет: {totalScore}";
    }

    public void CloseScore()
    {
        soundButton.GetComponent<AudioSource>().Play();
        toDestroy = GameObject.FindGameObjectsWithTag("Flower");
        foreach (var item in toDestroy) //Перебираем цветы на сцене
            Destroy(item);
        addedFlowers.Clear();
        flowerColors.Clear();
        scoreScreen.SetActive(false);
        isOpen = false;
        totalScore = 0;
        starCount = 0;
        sizeSlider.GetComponent<RectTransform>().localPosition = new Vector3(-150, 0, 0);
        positionArrow.transform.eulerAngles = new Vector3(0, 0, 0);
        GetComponent<SelectType>().InitSelection();
    }

    public void AddFlower(GameObject flower)
    {
        addedFlowers.Add(flower);
    }

    public void AddColor(FlowerColor color)
    {
        flowerColors.Add(color);
    }

    public void RemoveFromScoring()
    {
        int greenPre = 0;
        int greenNow = 0;
        foreach (var item in addedFlowers)
        {
            if (item.GetComponent<SpriteRenderer>().sortingLayerName == "Green")
                greenPre++;            
        }        
        addedFlowers.RemoveAt(addedFlowers.Count - 1);
        foreach (var item in addedFlowers)
        {
            if (item.GetComponent<SpriteRenderer>().sortingLayerName == "Green")
                greenNow++;
        }
        if (greenNow == greenPre)
            flowerColors.RemoveAt(flowerColors.Count - 1);
    }

    public void RemoveFromScoring(int id)
    {
        int greenPre = 0;
        int greenNow = 0;
        foreach (var item in addedFlowers)
        {
            if (item.GetComponent<SpriteRenderer>().sortingLayerName == "Green")
                greenPre++;
        }
        addedFlowers.RemoveAt(id);
        foreach (var item in addedFlowers)
        {
            if (item.GetComponent<SpriteRenderer>().sortingLayerName == "Green")
                greenNow++;
        }
        if (greenNow == greenPre)
            flowerColors.RemoveAt(flowerColors.Count - 1);
    }

    public bool CheckIsOpen()
    {
        return isOpen;
    }
}
