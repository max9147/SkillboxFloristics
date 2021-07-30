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
    public GameObject positionArrow;
    public GameObject MonochromeMeter;
    public GameObject ContrastMeter;
    public GameObject HarmonyMeter;
    public GameObject ContrastHarmonyMeter;
    public GameObject yellowColor;
    public GameObject orangeColor;
    public GameObject redColor;
    public GameObject violetColor;
    public GameObject blueColor;
    public GameObject greenColor;
    public GameObject whiteColor;
    public GameObject endHelpMenu;
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
    public TextMeshProUGUI colorScoreTextNumber;
    public TextMeshProUGUI balanceScoreText;
    public TextMeshProUGUI balanceScoreTextNumber;
    public TextMeshProUGUI endHelpText;
    public Slider sizeSlider;

    private GameObject[] toDestroy;
    private FlowerColor most;
    private int contrastMaxCombo = 0;
    private int harmonyMaxCombo = 0;
    private int contrastHarmonyMaxCombo = 0;
    private int focusCount = 0;
    private int baseCount = 0;
    private int fillCount = 0;
    private int detailsCount = 0;
    private int greenCount = 0;
    private int redColored = 0;
    private int yellowColored = 0;
    private int blueColored = 0;
    private int greenColored = 0;
    private int violetColored = 0;
    private int orangeColored = 0;
    private int whiteColored = 0;
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
    private bool[] endHelpStatus = { false, false, false, false, false };
    private string colorString;
    private string[] endHelpStrings = { "Попробуй использовать каждый вид цветов для создания совершенного букета.\n", "Букет будет смотреться красивее, если использовать цветы одной цветовой гаммы.\n",
        "Букет будет смотреться лучше, если использовать только противоположные цвета.\n", "Нарушение правильных пропорций в количестве цветов пойдет букету только на пользу.\n",
        "Букет будет смотреться совершеннее, если использовать цвета, которые в круге составляют треугольник.\n"};

    private int CheckType()
    {
        foreach (var item in addedFlowers)
        {
            switch (item.GetComponent<SelectFlowers>().type)
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

        if (focusCount == 0 || baseCount == 0 || fillCount == 0 || detailsCount == 0 || greenCount == 0) endHelpStatus[0] = true;

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
        if (flowerColors.Count == 0)
        {
            colorString = "Нет цветовой гаммы";
            return 0;
        }
        foreach (var item in flowerColors)
        {
            switch (item)
            {
                case FlowerColor.Yellow:
                    yellowColored++;
                    break;
                case FlowerColor.Orange:
                    orangeColored++;
                    break;
                case FlowerColor.Red:
                    redColored++;
                    break;
                case FlowerColor.Violet:
                    violetColored++;
                    break;
                case FlowerColor.Blue:
                    blueColored++;
                    break;
                case FlowerColor.Green:
                    greenColored++;
                    break;
                case FlowerColor.White:
                    whiteColored++;
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
                if (monochromeCount < flowerColors.Count) endHelpStatus[1] = true;
                colorString = "Монохромная цветовая гамма";
                break;
            case 1:
                colorScore = CheckContrast();
                if (contrastCount < flowerColors.Count) endHelpStatus[2] = true;
                if (colorScore <= 1000) endHelpStatus[3] = true;
                colorString = "Контрастная цветовая гамма";
                break;
            case 2:
                colorScore = CheckHarmony();
                if (colorScore <= 1500) endHelpStatus[3] = true;
                if (harmonyCount < flowerColors.Count) endHelpStatus[4] = true;
                colorString = "Гармоническая цветовая гамма";
                break;
            case 3:
                colorScore = CheckContrastHarmony();
                if (colorScore <= 1500) endHelpStatus[3] = true;
                if (harmonyCount < flowerColors.Count) endHelpStatus[4] = true;
                colorString = "Контрастная гармоническая цветовая гамма";
                break;
            default:
                break;
        }

        yellowColored = 0;
        orangeColored = 0;
        redColored = 0;
        violetColored = 0;
        blueColored = 0;
        greenColored = 0;
        whiteColored = 0;

        return colorScore;
    }

    private int CheckMonochrome()
    {
        int score = 1000;
        most = flowerColors.GroupBy(i => i).OrderByDescending(grp => grp.Count())
      .Select(grp => grp.Key).First();
        foreach (var item in flowerColors)
        {
            if (item != most) score -= 30;
            else monochromeCount++;
        }
        return score;
    }

    private int CheckContrast()
    {
        int score = 2000;
        int yellowViolet = 0;
        int orangeBlue = 0;
        int redGreen = 0;
        foreach (var item in flowerColors)
        {
            switch (item)
            {
                case FlowerColor.Yellow:
                    yellowViolet++;
                    break;
                case FlowerColor.Orange:
                    orangeBlue++;
                    break;
                case FlowerColor.Red:
                    redGreen++;
                    break;
                case FlowerColor.Violet:
                    yellowViolet++;
                    break;
                case FlowerColor.Blue:
                    orangeBlue++;
                    break;
                case FlowerColor.Green:
                    redGreen++;
                    break;
                default:
                    break;
            }
        }

        if (yellowColored == 0 || violetColored == 0) yellowViolet = 0;
        if (orangeColored == 0 || blueColored == 0) orangeBlue = 0;
        if (redColored == 0 || greenColored == 0) redGreen = 0;

        int[] combos = { yellowViolet, orangeBlue, redGreen };
        int max = 0;
        contrastMaxCombo = 3;
        for (int i = 0; i < combos.Length; i++)
        {
            if (combos[i] > max)
            {
                max = combos[i];
                contrastMaxCombo = i;
            }
        }
        contrastCount = max;
        switch (contrastMaxCombo)
        {
            case 0:
                if (yellowColored == violetColored) score = 1000;
                break;
            case 1:
                if (orangeColored == blueColored) score = 1000;
                break;
            case 2:
                if (redColored == greenColored) score = 1000;
                break;
            default:
                break;
        }
        score -= 40 * (flowerColors.Count - max);
        return score;
    }

    private int CheckHarmony()
    {
        int score = 3000;
        int yellowRedBlue = 0;
        int orangeVioletGreen = 0;
        foreach (var item in flowerColors)
        {
            switch (item)
            {
                case FlowerColor.Yellow:
                    yellowRedBlue++;
                    break;
                case FlowerColor.Orange:
                    orangeVioletGreen++;
                    break;
                case FlowerColor.Red:
                    yellowRedBlue++;
                    break;
                case FlowerColor.Violet:
                    orangeVioletGreen++;
                    break;
                case FlowerColor.Blue:
                    yellowRedBlue++;
                    break;
                case FlowerColor.Green:
                    orangeVioletGreen++;
                    break;
                default:
                    break;
            }
        }

        if (yellowColored == 0 || redColored == 0 || blueColored == 0) yellowRedBlue = 0;
        if (orangeColored == 0 || violetColored == 0 || greenColored == 0) orangeVioletGreen = 0;

        int[] combos = { yellowRedBlue, orangeVioletGreen };
        int max = 0;
        harmonyMaxCombo = 2;
        for (int i = 0; i < combos.Length; i++)
        {
            if (combos[i] > max)
            {
                max = combos[i];
                harmonyMaxCombo = i;
            }
        }
        harmonyCount = max;
        switch (harmonyMaxCombo)
        {
            case 0:
                if ((yellowColored == redColored) && (yellowColored == blueColored)) score = 1500;
                break;
            case 1:
                if ((orangeColored == violetColored) && (orangeColored == greenColored)) score = 1500;
                break;
            default:
                break;
        }
        score -= 60 * (flowerColors.Count - max);
        return score;
    }

    private int CheckContrastHarmony()
    {
        int score = 3000;
        int yellowVioletBlue = 0;
        int yellowVioletRed = 0;
        int orangeBlueGreen = 0;
        int orangeBlueViolet = 0;
        int redGreenYellow = 0;
        int redGreenBlue = 0;
        int violetYellowGreen = 0;
        int violetYellowOrange = 0;
        int blueOrangeRed = 0;
        int blueOrangeYellow = 0;
        int greenRedViolet = 0;
        int greenRedOrange = 0;
        foreach (var item in flowerColors)
        {
            switch (item)
            {
                case FlowerColor.Yellow:
                    yellowVioletBlue++;
                    yellowVioletRed++;
                    redGreenYellow++;
                    violetYellowGreen++;
                    violetYellowOrange++;
                    blueOrangeYellow++;
                    break;
                case FlowerColor.Orange:
                    orangeBlueGreen++;
                    orangeBlueViolet++;
                    blueOrangeRed++;
                    blueOrangeYellow++;
                    greenRedOrange++;
                    violetYellowOrange++;
                    break;
                case FlowerColor.Red:
                    redGreenBlue++;
                    redGreenYellow++;
                    blueOrangeRed++;
                    greenRedOrange++;
                    greenRedViolet++;
                    yellowVioletRed++;
                    break;
                case FlowerColor.Violet:
                    violetYellowGreen++;
                    violetYellowOrange++;
                    greenRedViolet++;
                    orangeBlueViolet++;
                    yellowVioletBlue++;
                    yellowVioletRed++;
                    break;
                case FlowerColor.Blue:
                    blueOrangeRed++;
                    blueOrangeYellow++;
                    orangeBlueGreen++;
                    orangeBlueViolet++;
                    redGreenBlue++;
                    yellowVioletBlue++;
                    break;
                case FlowerColor.Green:
                    greenRedOrange++;
                    greenRedViolet++;
                    orangeBlueGreen++;
                    redGreenBlue++;
                    redGreenYellow++;
                    violetYellowGreen++;
                    break;
                default:
                    break;
            }
        }

        if (yellowColored == 0 || violetColored == 0 || blueColored == 0) yellowVioletBlue = 0;
        if (yellowColored == 0 || violetColored == 0 || redColored == 0) yellowVioletRed = 0;
        if (orangeColored == 0 || blueColored == 0 || greenColored == 0) orangeBlueGreen = 0;
        if (orangeColored == 0 || blueColored == 0 || violetColored == 0) orangeBlueViolet = 0;
        if (redColored == 0 || greenColored == 0 || yellowColored == 0) redGreenYellow = 0;
        if (redColored == 0 || greenColored == 0 || blueColored == 0) redGreenBlue = 0;
        if (violetColored == 0 || yellowColored == 0 || greenColored == 0) violetYellowGreen = 0;
        if (violetColored == 0 || yellowColored == 0 || orangeColored == 0) violetYellowOrange = 0;
        if (blueColored == 0 || orangeColored == 0 || redColored == 0) blueOrangeRed = 0;
        if (blueColored == 0 || orangeColored == 0 || yellowColored == 0) blueOrangeYellow = 0;
        if (greenColored == 0 || redColored == 0 || violetColored == 0) greenRedViolet = 0;
        if (greenColored == 0 || redColored == 0 || orangeColored == 0) greenRedOrange = 0;

        int[] combos = { yellowVioletBlue, yellowVioletRed, orangeBlueGreen, orangeBlueViolet, redGreenYellow, redGreenBlue, violetYellowGreen,
            violetYellowOrange, blueOrangeRed, blueOrangeYellow, greenRedViolet, greenRedOrange };
        int max = 0;
        contrastHarmonyMaxCombo = 12;
        for (int i = 0; i < combos.Length; i++)
        {
            if (combos[i] > max)
            {
                max = combos[i];
                contrastHarmonyMaxCombo = i;
            }
        }
        contrastHarmonyCount = max;
        switch (contrastHarmonyMaxCombo)
        {
            case 0:
                if ((yellowColored == violetColored) && (yellowColored == blueColored)) score = 1500;
                break;
            case 1:
                if ((yellowColored == violetColored) && (yellowColored == redColored)) score = 1500;
                break;
            case 2:
                if ((orangeColored == blueColored) && (orangeColored == greenColored)) score = 1500;
                break;
            case 3:
                if ((orangeColored == blueColored) && (orangeColored == violetColored)) score = 1500;
                break;
            case 4:
                if ((redColored == greenColored) && (redColored == yellowColored)) score = 1500;
                break;
            case 5:
                if ((redColored == greenColored) && (redColored == blueColored)) score = 1500;
                break;
            case 6:
                if ((violetColored == yellowColored) && (violetColored == greenColored)) score = 1500;
                break;
            case 7:
                if ((violetColored == yellowColored) && (violetColored == orangeColored)) score = 1500;
                break;
            case 8:
                if ((blueColored == orangeColored) && (blueColored == redColored)) score = 1500;
                break;
            case 9:
                if ((blueColored == orangeColored) && (blueColored == yellowColored)) score = 1500;
                break;
            case 10:
                if ((greenColored == redColored) && (greenColored == violetColored)) score = 1500;
                break;
            case 11:
                if ((greenColored == redColored) && (greenColored == orangeColored)) score = 1500;
                break;
            default:
                break;
        }
        score -= 60 * (flowerColors.Count - max);
        return score;
    }

    private int CheckBalance()
    {
        if (positionArrow.transform.eulerAngles.z < 20 || positionArrow.transform.eulerAngles.z > 340)
        {
            balanceScoreText.text = "Отличный горизонтальный баланс";
            balanceScoreTextNumber.text = "500";
            return 500;
        }
        else if (positionArrow.transform.eulerAngles.z < 60 || positionArrow.transform.eulerAngles.z > 300)
        {
            balanceScoreText.text = "Хороший горизонтальный баланс";
            balanceScoreTextNumber.text = "250";
            return 250;
        }
        else
        {
            balanceScoreText.text = "Плохой горизонтальный баланс";
            balanceScoreTextNumber.text = "0";
            return 0;
        }
    }

    public void CountScore()
    {
        totalScore += CheckType();
        totalScore += CheckColor();
        totalScore += CheckBalance();
        scoreScreen.SetActive(true);
        isOpen = true;
        for (int i = 0; i < starCount; i++) stars[i].GetComponent<Image>().sprite = starActive;
        flowerCountText.text = addedFlowers.Count.ToString();
        focusCountText.text = focusCount.ToString();
        focusScoreText.text = $"{focusScore} очков";
        focusCount = 0;
        baseCountText.text = baseCount.ToString();
        baseScoreText.text = $"{baseScore} очков";
        baseCount = 0;
        fillCountText.text = fillCount.ToString();
        fillScoreText.text = $"{fillScore} очков";
        fillCount = 0;
        detailsCountText.text = detailsCount.ToString();
        detailsScoreText.text = $"{detailsScore} очков";
        detailsCount = 0;
        greenCountText.text = greenCount.ToString();
        greenScoreText.text = $"{greenScore} очков";
        greenCount = 0;
        colorScoreText.text = colorString.ToString();
        colorScoreTextNumber.text = CheckColor().ToString();
        totalScoreText.text = totalScore.ToString();
        for (int i = 0; i < endHelpStatus.Length; i++)
        {
            if (endHelpStatus[i])
            {
                endHelpText.text += endHelpStrings[i];
            }
        }
    }

    public void CloseScore()
    {
        for (int i = 0; i < endHelpStatus.Length; i++)
        {
            endHelpStatus[i] = false;
        }
        endHelpMenu.SetActive(false);
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
        sizeSlider.value = 0;
        positionArrow.transform.eulerAngles = new Vector3(0, 0, 0);
        GetComponent<SelectType>().InitSelection();
        CheckColorMeter();
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
            flowerColors.RemoveAt(id);
    }

    public void CheckColorMeter()
    {
        MonochromeMeter.SetActive(false);
        ContrastMeter.SetActive(false);
        HarmonyMeter.SetActive(false);
        ContrastHarmonyMeter.SetActive(false);

        if (flowerColors.Count == 0)
        {
            CheckSingleColors();
            return;
        }

        foreach (var item in flowerColors)
        {
            switch (item)
            {
                case FlowerColor.Yellow:
                    yellowColored++;
                    break;;
                case FlowerColor.Orange:
                    orangeColored++;
                    break;
                case FlowerColor.Red:
                    redColored++;
                    break;
                case FlowerColor.Violet:
                    violetColored++;
                    break;
                case FlowerColor.Blue:
                    blueColored++;
                    break;
                case FlowerColor.Green:
                    greenColored++;
                    break;
                case FlowerColor.White:
                    whiteColored++;
                    break;
                default:
                    break;
            }
        }

        CheckSingleColors();
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
        switch (maxCombo)
        {
            case 0:
                MonochromeMeter.SetActive(true);
                if (most == FlowerColor.White) MonochromeMeter.transform.localPosition = new Vector3(0, -50, 0);
                else MonochromeMeter.transform.localPosition = new Vector3(0, 0, 0);
                switch (most)
                {
                    case FlowerColor.Yellow:
                        MonochromeMeter.transform.eulerAngles = new Vector3(0, 0, 0);
                        break;
                    case FlowerColor.Orange:
                        MonochromeMeter.transform.eulerAngles = new Vector3(0, 0, -60);
                        break;
                    case FlowerColor.Red:
                        MonochromeMeter.transform.eulerAngles = new Vector3(0, 0, -120);
                        break;
                    case FlowerColor.Violet:
                        MonochromeMeter.transform.eulerAngles = new Vector3(0, 0, 180);
                        break;
                    case FlowerColor.Blue:
                        MonochromeMeter.transform.eulerAngles = new Vector3(0, 0, 120);
                        break;
                    case FlowerColor.Green:
                        MonochromeMeter.transform.eulerAngles = new Vector3(0, 0, 60);
                        break;
                    default:
                        MonochromeMeter.transform.eulerAngles = new Vector3(0, 0, 0);
                        break;
                }
                break;
            case 1:
                ContrastMeter.SetActive(true);
                switch (contrastMaxCombo)
                {
                    case 0:
                        ContrastMeter.transform.eulerAngles = new Vector3(0, 0, 0);
                        break;
                    case 1:
                        ContrastMeter.transform.eulerAngles = new Vector3(0, 0, -60);
                        break;
                    case 2:
                        ContrastMeter.transform.eulerAngles = new Vector3(0, 0, -120);
                        break;
                    default:
                        break;
                }
                break;
            case 2:
                HarmonyMeter.SetActive(true);
                switch (harmonyMaxCombo)
                {
                    case 0:
                        HarmonyMeter.transform.eulerAngles = new Vector3(0, 0, 0);
                        break;
                    case 1:
                        HarmonyMeter.transform.eulerAngles = new Vector3(0, 0, 60);
                        break;
                    default:
                        break;
                }
                break;
            case 3:
                ContrastHarmonyMeter.SetActive(true);
                switch (contrastHarmonyMaxCombo)
                {
                    case 0:
                        ContrastHarmonyMeter.transform.eulerAngles = new Vector3(0, 0, 0);
                        ContrastHarmonyMeter.transform.localScale = new Vector3(1, 1, 1);
                        break;
                    case 1:
                        ContrastHarmonyMeter.transform.eulerAngles = new Vector3(0, 0, 0);
                        ContrastHarmonyMeter.transform.localScale = new Vector3(-1, 1, 1);
                        break;
                    case 2:
                        ContrastHarmonyMeter.transform.eulerAngles = new Vector3(0, 0, -60);
                        ContrastHarmonyMeter.transform.localScale = new Vector3(1, 1, 1);
                        break;
                    case 3:
                        ContrastHarmonyMeter.transform.eulerAngles = new Vector3(0, 0, -60);
                        ContrastHarmonyMeter.transform.localScale = new Vector3(-1, 1, 1);
                        break;
                    case 4:
                        ContrastHarmonyMeter.transform.eulerAngles = new Vector3(0, 0, -120);
                        ContrastHarmonyMeter.transform.localScale = new Vector3(1, 1, 1);
                        break;
                    case 5:
                        ContrastHarmonyMeter.transform.eulerAngles = new Vector3(0, 0, -120);
                        ContrastHarmonyMeter.transform.localScale = new Vector3(-1, 1, 1);
                        break;
                    case 6:
                        ContrastHarmonyMeter.transform.eulerAngles = new Vector3(0, 0, 180);
                        ContrastHarmonyMeter.transform.localScale = new Vector3(1, 1, 1);
                        break;
                    case 7:
                        ContrastHarmonyMeter.transform.eulerAngles = new Vector3(0, 0, 180);
                        ContrastHarmonyMeter.transform.localScale = new Vector3(-1, 1, 1);
                        break;
                    case 8:
                        ContrastHarmonyMeter.transform.eulerAngles = new Vector3(0, 0, 120);
                        ContrastHarmonyMeter.transform.localScale = new Vector3(1, 1, 1);
                        break;
                    case 9:
                        ContrastHarmonyMeter.transform.eulerAngles = new Vector3(0, 0, 120);
                        ContrastHarmonyMeter.transform.localScale = new Vector3(-1, 1, 1);
                        break;
                    case 10:
                        ContrastHarmonyMeter.transform.eulerAngles = new Vector3(0, 0, 60);
                        ContrastHarmonyMeter.transform.localScale = new Vector3(1, 1, 1);
                        break;
                    case 11:
                        ContrastHarmonyMeter.transform.eulerAngles = new Vector3(0, 0, 60);
                        ContrastHarmonyMeter.transform.localScale = new Vector3(-1, 1, 1);
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
        monochromeCount = 0;
        contrastCount = 0;
        harmonyCount = 0;
        contrastHarmonyCount = 0;
        yellowColored = 0;
        orangeColored = 0;
        redColored = 0;
        violetColored = 0;
        blueColored = 0;
        greenColored = 0;
        whiteColored = 0;
    }

    public void CheckSingleColors()
    {
        if (yellowColored > 0) yellowColor.SetActive(true);
        else yellowColor.SetActive(false);
        if (orangeColored > 0) orangeColor.SetActive(true);
        else orangeColor.SetActive(false);
        if (redColored > 0) redColor.SetActive(true);
        else redColor.SetActive(false);
        if (violetColored > 0) violetColor.SetActive(true);
        else violetColor.SetActive(false);
        if (blueColored > 0) blueColor.SetActive(true);
        else blueColor.SetActive(false);
        if (greenColored > 0) greenColor.SetActive(true);
        else greenColor.SetActive(false);
        if (whiteColored > 0) whiteColor.SetActive(true);
        else whiteColor.SetActive(false);
    }

    public void ChangeEndHelpState()
    {
        soundButton.GetComponent<AudioSource>().Play();
        if (endHelpMenu.activeInHierarchy) endHelpMenu.SetActive(false);
        else endHelpMenu.SetActive(true);
    }

    public bool CheckIsOpen()
    {
        return isOpen;
    }
}