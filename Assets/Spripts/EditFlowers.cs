using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditFlowers : MonoBehaviour
{
    public GameObject editMenu;
    public GameObject canvas;
    public GameObject bouquet;
    public GameObject basket;
    public GameObject positionArrow;
    public GameObject soundButton;
    public Camera mainCam;
    public Button buttonCreate;
    public Button buttonBack;
    public Slider sizeSlider;
    public TextMeshProUGUI amountText;
    public bool isDragging = false;

    private GameObject prevFlower;
    private GameObject curMenu;
    private GameObject[] flowers;
    private bool isEditing = false;

    public void SelectFlower(GameObject flower)
    {
        if (flower.GetComponent<SpriteRenderer>().sortingLayerName == "Default" || isEditing || GetComponent<ScoringSystem>().CheckIsOpen()) return;
        prevFlower = flower;      
        flower.GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";
        flower.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
    }

    public void DisselectFlower()
    {
        if (prevFlower && !isEditing)
        {
            prevFlower.GetComponent<SpriteRenderer>().sortingLayerName = "Flowers";
            prevFlower.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        }
    }

    public void StartEdit(GameObject flower)
    {        
        if (curMenu || GetComponent<ScoringSystem>().CheckIsOpen()) return;
        flower.GetComponent<SpriteRenderer>().sortingLayerName = "Flowers";
        isEditing = true;
        curMenu = Instantiate(editMenu, canvas.transform);
        Vector3 curPos = new Vector3(mainCam.ScreenToWorldPoint(Input.mousePosition).x, mainCam.ScreenToWorldPoint(Input.mousePosition).y, 0); //Текущая позиция курсора
        if (curPos.y >= -2.2f) curMenu.transform.position = curPos - new Vector3(0, 0.7f, 0);
        else curMenu.transform.position = new Vector3(curPos.x, -2.9f, 0);
        curMenu.transform.Find("ExitButton").GetComponentInChildren<Button>().onClick.AddListener(delegate { this.StopEdit(); });
        curMenu.transform.Find("RemoveButton").GetComponentInChildren<Button>().onClick.AddListener(delegate { this.RemoveFlower(flower); });
        curMenu.transform.Find("LayerUpButton").GetComponentInChildren<Button>().onClick.AddListener(delegate { this.LayerUp(flower); });
        curMenu.transform.Find("LayerDownButton").GetComponentInChildren<Button>().onClick.AddListener(delegate { this.LayerDown(flower); });
    }

    public void StopEdit()
    {
        isEditing = false;
        Destroy(curMenu);
    }

    public void RemoveFlower(GameObject flower)
    {
        soundButton.GetComponent<AudioSource>().Play();
        isEditing = false;
        Destroy(flower);
        Destroy(curMenu);

        if ((bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() >= 1 && bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() <= 30) || (basket.GetComponent<ArrangeBasket>().GetFlowerCount() >= 1 && basket.GetComponent<ArrangeBasket>().GetFlowerCount() <= 30))
            sizeSlider.value -= 0.033f;

        GetComponent<ScoringSystem>().RemoveFromScoring(flower.GetComponent<SpriteRenderer>().sortingOrder - 1);        

        flowers = GameObject.FindGameObjectsWithTag("Flower");
        if (bouquet.activeInHierarchy)
        {
            float rotation = bouquet.GetComponent<ArrangeBouquet>().GetRotation(flower.GetComponent<SpriteRenderer>().sortingOrder - 1);
            if (rotation > 90) positionArrow.transform.eulerAngles = new Vector3(0, 0, 90);
            else if (rotation < -90) positionArrow.transform.eulerAngles = new Vector3(0, 0, -90);
            else positionArrow.transform.eulerAngles = new Vector3(0, 0, rotation);
            foreach (var item in flowers) //Перебираем цветы на сцене
            {
                if (item.GetComponent<SpriteRenderer>().sortingOrder == bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount()) //Находим цветы
                    bouquet.GetComponent<ArrangeBouquet>().RemoveFlower();

                if (bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() == 0)
                {
                    buttonBack.interactable = false;
                }

                if (bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() < 10 || bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() > 30)
                    buttonCreate.interactable = false;
            }
        }
        if (basket.activeInHierarchy)
        {
            float rotation = basket.GetComponent<ArrangeBasket>().GetRotation(flower.GetComponent<SpriteRenderer>().sortingOrder - 1);
            if (rotation > 90) positionArrow.transform.eulerAngles = new Vector3(0, 0, 90);
            else if (rotation < -90) positionArrow.transform.eulerAngles = new Vector3(0, 0, -90);
            else positionArrow.transform.eulerAngles = new Vector3(0, 0, rotation);
            foreach (var item in flowers) //Перебираем цветы на сцене
            {
                if (item.GetComponent<SpriteRenderer>().sortingOrder == basket.GetComponent<ArrangeBasket>().GetFlowerCount()) //Находим цветы
                    basket.GetComponent<ArrangeBasket>().RemoveFlower();

                if (basket.GetComponent<ArrangeBasket>().GetFlowerCount() == 0)
                {
                    buttonBack.interactable = false;
                }

                if (basket.GetComponent<ArrangeBasket>().GetFlowerCount() < 10 || basket.GetComponent<ArrangeBasket>().GetFlowerCount() > 30)
                    buttonCreate.interactable = false;
            }
        }

        if (bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() > 30 || basket.GetComponent<ArrangeBasket>().GetFlowerCount() > 30) amountText.text = "Много цветов";
        else if (bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() >= 10 || basket.GetComponent<ArrangeBasket>().GetFlowerCount() >= 10) amountText.text = "";
        else amountText.text = "Мало цветов";

        if ((bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() >= 10 && bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() <= 30) || (basket.GetComponent<ArrangeBasket>().GetFlowerCount() >= 10 && basket.GetComponent<ArrangeBasket>().GetFlowerCount() <= 30))
            buttonCreate.interactable = true;

        foreach (var item in flowers) //Перебираем цветы на сцене
            if (item.GetComponent<SpriteRenderer>().sortingOrder > flower.GetComponent<SpriteRenderer>().sortingOrder) item.GetComponent<SpriteRenderer>().sortingOrder--;

        GetComponent<ScoringSystem>().CheckColorMeter();
        GetComponent<LogSystem>().RemoveFromLog(flower.GetComponent<SpriteRenderer>().sortingOrder);
    }

    public void LayerUp(GameObject flower)
    {
        soundButton.GetComponent<AudioSource>().Play();        
        flowers = GameObject.FindGameObjectsWithTag("Flower");
        foreach (var item in flowers) //Перебираем цветы на сцене
        {
            if (item.GetComponent<SpriteRenderer>().sortingOrder == flower.GetComponent<SpriteRenderer>().sortingOrder + 1) //Находим цветы
            {
                GetComponent<LogSystem>().LayerUpLog(flower.GetComponent<SpriteRenderer>().sortingOrder);
                item.GetComponent<SpriteRenderer>().sortingOrder--;
                flower.GetComponent<SpriteRenderer>().sortingOrder++;
                break;
            }
        }
    }

    public void LayerDown(GameObject flower)
    {
        soundButton.GetComponent<AudioSource>().Play();        
        flowers = GameObject.FindGameObjectsWithTag("Flower");
        foreach (var item in flowers) //Перебираем цветы на сцене
        {
            if (item.GetComponent<SpriteRenderer>().sortingOrder == flower.GetComponent<SpriteRenderer>().sortingOrder - 1) //Находим цветы
            {
                GetComponent<LogSystem>().LayerDownLog(flower.GetComponent<SpriteRenderer>().sortingOrder);
                item.GetComponent<SpriteRenderer>().sortingOrder++;
                flower.GetComponent<SpriteRenderer>().sortingOrder--;
                break;
            }
        }
    }
}