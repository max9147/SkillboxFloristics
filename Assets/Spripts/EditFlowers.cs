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
    public GameObject sizeSlider;
    public GameObject positionArrow;
    public Camera mainCam;
    public Button buttonCreate;
    public Button buttonReset;
    public Button buttonBack;

    private GameObject prevFlower;
    private GameObject curMenu;
    private GameObject[] flowers;
    private string prevLayer;
    private bool isEditing = false;

    public void SelectFlower(GameObject flower)
    {
        if (flower.GetComponent<SpriteRenderer>().sortingLayerName == "Default" || isEditing) return;
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
        if (curMenu) return;
        isEditing = false;
        DisselectFlower();
        SelectFlower(flower);
        isEditing = true;
        curMenu = Instantiate(editMenu, canvas.transform);
        Vector3 curPos = new Vector3(mainCam.ScreenToWorldPoint(Input.mousePosition).x, mainCam.ScreenToWorldPoint(Input.mousePosition).y, 0); //������� ������� �������
        curMenu.transform.position = curPos - new Vector3(1, 1.5f, 0);
        curMenu.transform.Find("ExitButton").GetComponentInChildren<Button>().onClick.AddListener(delegate { this.StopEdit(); });
        curMenu.transform.Find("RemoveButton").GetComponentInChildren<Button>().onClick.AddListener(delegate { this.RemoveFlower(flower); });
        curMenu.transform.Find("LayerUpButton").GetComponentInChildren<Button>().onClick.AddListener(delegate { this.LayerUp(flower); });
        curMenu.transform.Find("LayerDownButton").GetComponentInChildren<Button>().onClick.AddListener(delegate { this.LayerDown(flower); });
    }

    public void StopEdit()
    {
        isEditing = false;
        DisselectFlower();
        Destroy(curMenu);
    }

    public void RemoveFlower(GameObject flower)
    {
        isEditing = false;
        Destroy(flower);
        Destroy(curMenu);

        if ((bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() >= 1 && bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() <= 30) || (basket.GetComponent<ArrangeBasket>().GetFlowerCount() >= 1 && basket.GetComponent<ArrangeBasket>().GetFlowerCount() <= 30))
            sizeSlider.GetComponent<RectTransform>().localPosition -= new Vector3(10, 0, 0);
        GetComponent<ScoringSystem>().RemoveFromScoring(flower.GetComponent<SpriteRenderer>().sortingOrder - 1);

        flowers = GameObject.FindGameObjectsWithTag("Flower");
        if (bouquet.activeInHierarchy)
        {
            float rotation = bouquet.GetComponent<ArrangeBouquet>().GetRotation(flower.GetComponent<SpriteRenderer>().sortingOrder - 1);
            if (rotation > 90) positionArrow.transform.eulerAngles = new Vector3(0, 0, 90);
            else if (rotation < -90) positionArrow.transform.eulerAngles = new Vector3(0, 0, -90);
            else positionArrow.transform.eulerAngles = new Vector3(0, 0, rotation);
            foreach (var item in flowers) //���������� ����� �� �����
            {
                if (item.GetComponent<SpriteRenderer>().sortingOrder == bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount()) //������� �����
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
            foreach (var item in flowers) //���������� ����� �� �����
            {
                if (item.GetComponent<SpriteRenderer>().sortingOrder == basket.GetComponent<ArrangeBasket>().GetFlowerCount()) //������� �����
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

        foreach (var item in flowers) //���������� ����� �� �����
            if (item.GetComponent<SpriteRenderer>().sortingOrder > flower.GetComponent<SpriteRenderer>().sortingOrder) item.GetComponent<SpriteRenderer>().sortingOrder--;
    }

    public void LayerUp(GameObject flower)
    {
        flower.GetComponent<SpriteRenderer>().sortingLayerName = prevLayer;
        flowers = GameObject.FindGameObjectsWithTag("Flower");
        foreach (var item in flowers) //���������� ����� �� �����
        {
            if (item.GetComponent<SpriteRenderer>().sortingOrder == flower.GetComponent<SpriteRenderer>().sortingOrder + 1) //������� �����
            {
                item.GetComponent<SpriteRenderer>().sortingOrder--;
                flower.GetComponent<SpriteRenderer>().sortingOrder++;
                break;
            }
        }
    }

    public void LayerDown(GameObject flower)
    {
        flower.GetComponent<SpriteRenderer>().sortingLayerName = prevLayer;
        flowers = GameObject.FindGameObjectsWithTag("Flower");
        foreach (var item in flowers) //���������� ����� �� �����
        {
            if (item.GetComponent<SpriteRenderer>().sortingOrder == flower.GetComponent<SpriteRenderer>().sortingOrder - 1) //������� �����
            {
                item.GetComponent<SpriteRenderer>().sortingOrder++;
                flower.GetComponent<SpriteRenderer>().sortingOrder--;
                break;
            }
        }
    }
}