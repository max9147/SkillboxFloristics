using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragFlowers : MonoBehaviour
{
    public Camera mainCam;
    public Flower flower;
    public GameObject flowerPrefab; //���������� ������ � ������� ��������
    public GameObject bouquet;
    public GameObject bouquetTip;
    public GameObject basket;
    public GameObject basketTip;
    public GameObject content; //������� �������� scrollview
    public GameObject desctiptionMenu;
    public GameObject gameManager;
    public Button buttonCreate;
    public Button buttonBack;

    private GameObject flowerToDrag;
    private GameObject curMenu;
    private Vector3 snappedPos;
    private Ray ray;
    private float contentPos;
    private bool isDragging = false; //���� �� � ������ � ������� ������
    private bool canTake = true; //����� �� ����� ������

    private void OnMouseDown()
    {
        ray = mainCam.ScreenPointToRay(Input.mousePosition); //�������� �� ���������� ������� � content
        if (canTake && !gameManager.GetComponent<ScoringSystem>().CheckIsOpen() && !gameManager.GetComponent<SelectType>().CheckIsOpen() && !gameManager.GetComponent<TabsChange>().CheckDescriptionOpen() && ray.origin.y < 2.7f && ray.origin.y > -1.7f)
        {
            Destroy(curMenu);
            gameManager.GetComponent<EditFlowers>().StopEdit();
            bouquet.transform.position = new Vector3(bouquet.transform.position.x, bouquet.transform.position.y, -0.01f);
            basket.transform.position = new Vector3(basket.transform.position.x, basket.transform.position.y, -0.01f);
            GetComponent<AudioSource>().Play();
            contentPos = content.GetComponent<RectTransform>().localPosition.y; //��������� content �� ����� ������ ������
            buttonCreate.interactable = false; //��������� ��������� ���������� ���� ����� ������
            gameManager.GetComponent<EditFlowers>().isDragging = true;
            isDragging = true;
            canTake = false;
            Vector3 spawnPos = new Vector3(mainCam.ScreenToWorldPoint(Input.mousePosition).x, mainCam.ScreenToWorldPoint(Input.mousePosition).y, 0); //���������� �������, �� z=0
            flowerToDrag = Instantiate(flowerPrefab, spawnPos, Quaternion.identity); //����� ������ � ������� ������� � ������ ��� � ����������
        }
    }

    private void OnMouseUp()
    {
        if (!flowerToDrag) return;
        isDragging = false;
        gameManager.GetComponent<EditFlowers>().isDragging = false;        
        if (bouquet.GetComponent<ArrangeBouquet>().CheckFlowerPos() || basket.GetComponent<ArrangeBasket>().CheckFlowerPos()) ReleaseFlower();
        bouquet.transform.position = new Vector3(bouquet.transform.position.x, bouquet.transform.position.y, 0.01f);
        basket.transform.position = new Vector3(basket.transform.position.x, basket.transform.position.y, 0.01f);
    }

    private void OnMouseEnter()
    {
        if (transform.position.y <= -1.8f || transform.position.y >= 2.5f) return;
        if (gameManager.GetComponent<TabsChange>().CheckDescriptionOpen() || gameManager.GetComponent<ScoringSystem>().CheckIsOpen() || gameManager.GetComponent<SelectType>().CheckIsOpen() || gameManager.GetComponent<EditFlowers>().isDragging) return;
        curMenu = Instantiate(desctiptionMenu, transform);
        curMenu.transform.Find("FlowerNameText").GetComponent<TextMeshProUGUI>().text = flower.flowerName;
        if (transform.position.y >= -0.85f) curMenu.transform.position = transform.position - new Vector3(0, 0.6f, 0);
        else
        {
            curMenu.transform.position = transform.position + new Vector3(0, 0.6f, 0);
            curMenu.transform.Find("Background").localScale = new Vector3(1, -1, 1);
        }
    }

    private void OnMouseExit()
    {
        Destroy(curMenu);
    }

    private void Update()
    {
        if (flowerToDrag)
        {
            content.GetComponent<RectTransform>().localPosition = new Vector3(0, contentPos, 0); //���������� content � ��������� � ������� �� ��� ��� ������ ������
        }
    }

    private void FixedUpdate()
    {
        if (flowerToDrag)
        {
            if (isDragging) //����������� ����� ����� ����� ������
            {
                Vector3 curPos = new Vector3(mainCam.ScreenToWorldPoint(Input.mousePosition).x, mainCam.ScreenToWorldPoint(Input.mousePosition).y, 0); //������� ������� �������

                if (bouquet.GetComponent<ArrangeBouquet>().CheckFlowerInside())
                {
                    flowerToDrag.GetComponent<SpriteRenderer>().sortingLayerName = flower.size; //���������� ������ �� ��� ����
                    flowerToDrag.GetComponent<SelectFlowers>().type = flowerToDrag.GetComponent<SpriteRenderer>().sortingLayerName;
                    flowerToDrag.GetComponent<SpriteRenderer>().sortingOrder = bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() + 1;
                    flowerToDrag.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;

                    if (flower.size == "Details")
                    {
                        snappedPos = new Vector3(curPos.x, curPos.y + 1, 0); //������� ����� ������ � ������
                    }
                    else
                    {
                        snappedPos = new Vector3((bouquet.transform.position.x + curPos.x) / 2, (bouquet.transform.position.y + curPos.y) / 2, 0); //������� ����� ������ � ������
                    }

                    flowerToDrag.transform.position = snappedPos;
                    flowerToDrag.transform.up = (bouquetTip.transform.position - flowerToDrag.transform.position) * -1; //������ ������ ������� ������ ������

                    if (bouquet.transform.position.x - flowerToDrag.transform.position.x > 0.35f) //���� ������ � ����� ����� ������
                    {
                        flowerToDrag.GetComponent<SpriteRenderer>().sprite = flower.imageL;
                    }
                    else if (bouquet.transform.position.x - flowerToDrag.transform.position.x < -0.35f) //���� ������ � ������ ����� ������
                    {
                        flowerToDrag.GetComponent<SpriteRenderer>().sprite = flower.imageR;
                    }
                    else //���� ������ � ������ ������
                    {
                        flowerToDrag.GetComponent<SpriteRenderer>().sprite = flower.imageC;
                    }
                }
                else if (basket.GetComponent<ArrangeBasket>().CheckFlowerInside())
                {
                    flowerToDrag.GetComponent<SpriteRenderer>().sortingLayerName = flower.size; //���������� ������ �� ��� ����
                    flowerToDrag.GetComponent<SelectFlowers>().type = flowerToDrag.GetComponent<SpriteRenderer>().sortingLayerName;
                    flowerToDrag.GetComponent<SpriteRenderer>().sortingOrder = basket.GetComponent<ArrangeBasket>().GetFlowerCount() + 1;
                    flowerToDrag.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                    flowerToDrag.transform.up = (basketTip.transform.position - flowerToDrag.transform.position) * -1; //������ ������ ������� ������ �������
                    snappedPos = new Vector3((basket.transform.position.x + curPos.x) / 2, curPos.y+2, 0); //������� ����� ������ � �������
                    flowerToDrag.transform.position = snappedPos;
                }
                else
                {
                    flowerToDrag.GetComponent<SpriteRenderer>().sortingLayerName = "Default"; //���������� ������ �� ����������� ���� ��� ��������� �� ������
                    flowerToDrag.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
                    flowerToDrag.GetComponent<SpriteRenderer>().sprite = flower.imageC;
                    flowerToDrag.transform.rotation = Quaternion.identity; //�������� �������� ������
                    flowerToDrag.transform.position = curPos;
                }
            }
            else
            {
                flowerToDrag.transform.position = Vector3.MoveTowards(flowerToDrag.transform.position, transform.position, 0.5f); //���������� ������ � ��� ��������
            }

            if (flowerToDrag.transform.position == transform.position) //����� ������ ������� �� ��������
            {
                canTake = true;
                Destroy(flowerToDrag);
                if ((bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() >= 10 && bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() <= 30) || (basket.GetComponent<ArrangeBasket>().GetFlowerCount() >= 10 && basket.GetComponent<ArrangeBasket>().GetFlowerCount() <= 30))
                {
                    buttonCreate.interactable = true;
                }
            }
        }
    }

    private void ReleaseFlower() //������� ������ ������������ � ������
    {
        gameManager.GetComponent<ScoringSystem>().CheckColorMeter();
        gameManager.GetComponent<ScoringSystem>().AddFlower(flowerToDrag);
        if (flower.size != "Green")
            gameManager.GetComponent<ScoringSystem>().AddColor(flower.color);
        flowerToDrag = null;
        canTake = true;
        buttonBack.interactable = true;
    }

    public void AllowTaking()
    {
        canTake = true;
    }
}