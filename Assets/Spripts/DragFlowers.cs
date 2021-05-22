using System.Collections;
using System.Collections.Generic;
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
    public GameObject gameManager;
    public Button buttonCreate;
    public Button buttonBack;

    private GameObject flowerToDrag;
    private Vector3 snappedPos;
    private Ray ray;
    private float contentPos;
    private bool isDragging = false; //���� �� � ������ � ������� ������
    private bool canTake = true; //����� �� ����� ������

    private void OnMouseDown()
    {
        ray = mainCam.ScreenPointToRay(Input.mousePosition); //�������� �� ���������� ������� � content
        if (canTake && !gameManager.GetComponent<ScoringSystem>().CheckIsOpen() && !gameManager.GetComponent<SelectType>().CheckIsOpen() && ray.origin.y < 3.7f && ray.origin.y > -2.9f)
        {
            GetComponent<AudioSource>().Play();
            contentPos = content.GetComponent<RectTransform>().localPosition.y; //��������� content �� ����� ������ ������
            buttonCreate.interactable = false; //��������� ��������� ���������� ���� ����� ������
            isDragging = true;
            canTake = false;
            Vector3 spawnPos = new Vector3(mainCam.ScreenToWorldPoint(Input.mousePosition).x, mainCam.ScreenToWorldPoint(Input.mousePosition).y, 0); //���������� �������, �� z=0
            flowerToDrag = Instantiate(flowerPrefab, spawnPos, Quaternion.identity); //����� ������ � ������� ������� � ������ ��� � ����������
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;

        if (bouquet.GetComponent<ArrangeBouquet>().CheckFlowerPos() || basket.GetComponent<ArrangeBasket>().CheckFlowerPos()) //�������� �� ���������� ������ � ������� ����������
        {            
            ReleaseFlower();
        }
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

                    flowerToDrag.transform.position = Vector3.MoveTowards(flowerToDrag.transform.position, snappedPos, 5); //���������� ������ � �����
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
                    flowerToDrag.GetComponent<SpriteRenderer>().sortingOrder = basket.GetComponent<ArrangeBasket>().GetFlowerCount() + 1;
                    flowerToDrag.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                    flowerToDrag.transform.up = (basketTip.transform.position - flowerToDrag.transform.position) * -1; //������ ������ ������� ������ �������
                    snappedPos = new Vector3((basket.transform.position.x + curPos.x) / 2, curPos.y+1, 0); //������� ����� ������ � �������
                    flowerToDrag.transform.position = Vector3.MoveTowards(flowerToDrag.transform.position, snappedPos, 5); //���������� ������ � �������
                }
                else
                {
                    flowerToDrag.GetComponent<SpriteRenderer>().sortingLayerName = "Default"; //���������� ������ �� ����������� ���� ��� ��������� �� ������
                    flowerToDrag.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
                    flowerToDrag.GetComponent<SpriteRenderer>().sprite = flower.imageC;
                    flowerToDrag.transform.rotation = Quaternion.identity; //�������� �������� ������
                    flowerToDrag.transform.position = Vector3.MoveTowards(flowerToDrag.transform.position, curPos, 5f); //���������� ������ � �������
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
                if ((bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() >= 20 && bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() <= 35) || (basket.GetComponent<ArrangeBasket>().GetFlowerCount() >= 25 && basket.GetComponent<ArrangeBasket>().GetFlowerCount() <= 35))
                {
                    buttonCreate.interactable = true;
                }
            }
        }
    }

    private void ReleaseFlower() //������� ������ ������������ � ������
    {
        gameManager.GetComponent<ScoringSystem>().AddFlower(flowerToDrag);
        flowerToDrag = null;
        canTake = true;
        buttonBack.interactable = true;
    }

    public void AllowTaking()
    {
        canTake = true;
    }
}