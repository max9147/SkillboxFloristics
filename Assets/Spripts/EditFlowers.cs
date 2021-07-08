using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditFlowers : MonoBehaviour
{
    public GameObject editMenu;
    public GameObject canvas;
    public GameObject bouquet;
    public GameObject basket;
    public GameObject positionArrow;
    public Camera mainCam;
    public Button buttonCreate;
    public Button buttonReset;
    public Button buttonBack;
    public Slider sizeSlider;
    public bool isDragging = false;

    private GameObject prevFlower;
    private GameObject curMenu;
    private GameObject[] flowers;
    private string prevLayer;
    private bool isEditing = false;

    public void SelectFlower(GameObject flower)
    {
        if (flower.GetComponent<SpriteRenderer>().sortingLayerName == "Default" || isEditing || GetComponent<ScoringSystem>().CheckIsOpen()) return;
        prevFlower = flower;
        prevLayer = flower.GetComponent<SpriteRenderer>().sortingLayerName;
        flower.GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";        
    }

    public void DisselectFlower()
    {
        if (prevFlower && !isEditing) prevFlower.GetComponent<SpriteRenderer>().sortingLayerName = prevLayer;
    }

    public void StartEdit(GameObject flower)
    {        
        if (curMenu || GetComponent<ScoringSystem>().CheckIsOpen()) return;
        flower.GetComponent<SpriteRenderer>().sortingLayerName = prevLayer;
        isEditing = true;
        curMenu = Instantiate(editMenu, canvas.transform);
        Vector3 curPos = new Vector3(mainCam.ScreenToWorldPoint(Input.mousePosition).x, mainCam.ScreenToWorldPoint(Input.mousePosition).y, 0); //Текущая позиция курсора
        curMenu.transform.position = curPos - new Vector3(0, 0.7f, 0);
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
                    buttonReset.interactable = false;
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
                    buttonReset.interactable = false;
                    buttonBack.interactable = false;
                }

                if (basket.GetComponent<ArrangeBasket>().GetFlowerCount() < 10 || basket.GetComponent<ArrangeBasket>().GetFlowerCount() > 30)
                    buttonCreate.interactable = false;
            }
        }
        if ((bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() >= 10 && bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() <= 30) || (basket.GetComponent<ArrangeBasket>().GetFlowerCount() >= 10 && basket.GetComponent<ArrangeBasket>().GetFlowerCount() <= 30))
            buttonCreate.interactable = true;

        foreach (var item in flowers) //Перебираем цветы на сцене
            if (item.GetComponent<SpriteRenderer>().sortingOrder > flower.GetComponent<SpriteRenderer>().sortingOrder) item.GetComponent<SpriteRenderer>().sortingOrder--;
    }

    public void LayerUp(GameObject flower)
    {
        flowers = GameObject.FindGameObjectsWithTag("Flower");
        foreach (var item in flowers) //Перебираем цветы на сцене
        {
            if (item.GetComponent<SpriteRenderer>().sortingOrder == flower.GetComponent<SpriteRenderer>().sortingOrder + 1) //Находим цветы
            {
                item.GetComponent<SpriteRenderer>().sortingOrder--;
                flower.GetComponent<SpriteRenderer>().sortingOrder++;
                if (item.GetComponent<SpriteRenderer>().sortingLayerName != flower.GetComponent<SpriteRenderer>().sortingLayerName)
                    flower.GetComponent<SpriteRenderer>().sortingLayerName = item.GetComponent<SpriteRenderer>().sortingLayerName;
                break;
            }
        }
    }

    public void LayerDown(GameObject flower)
    {
        flowers = GameObject.FindGameObjectsWithTag("Flower");
        foreach (var item in flowers) //Перебираем цветы на сцене
        {
            if (item.GetComponent<SpriteRenderer>().sortingOrder == flower.GetComponent<SpriteRenderer>().sortingOrder - 1) //Находим цветы
            {
                item.GetComponent<SpriteRenderer>().sortingOrder++;
                flower.GetComponent<SpriteRenderer>().sortingOrder--;
                if (item.GetComponent<SpriteRenderer>().sortingLayerName != flower.GetComponent<SpriteRenderer>().sortingLayerName)
                    flower.GetComponent<SpriteRenderer>().sortingLayerName = item.GetComponent<SpriteRenderer>().sortingLayerName;
                break;
            }
        }
    }
}