using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragFlowers : MonoBehaviour
{
    public Camera mainCam;
    public Flower flower;
    public GameObject flowerPrefab; //���������� ������ � ������� ��������
    public GameObject bouquet;
    public GameObject bouquetTip;
    public GameObject basket;
    public GameObject itemsContent; //������� �������� scrollview
    public Button buttonCreate;
    public Button buttonBack;

    private GameObject flowerToDrag;
    private Vector3 snappedPos;
    private float contentPos;
    private bool isDragging = false; //���� �� � ������ � ������� ������
    private bool canTake = true; //����� �� ����� ������

    private void OnMouseDown()
    {
        if (canTake)
        {
            contentPos = itemsContent.GetComponent<RectTransform>().localPosition.y; //��������� content �� ����� ������ ������
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
            itemsContent.GetComponent<RectTransform>().localPosition = new Vector3(0, contentPos, 0); //���������� content � ��������� � ������� �� ��� ��� ������ ������
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

                    if (flower.size == "Details")
                    {
                        snappedPos = new Vector3((bouquet.transform.position.x + curPos.x) / 2, (bouquet.transform.position.y + curPos.y) / 3 + 0.5f, 0);
                    }
                    else
                    {
                        snappedPos = new Vector3((bouquet.transform.position.x + curPos.x) / 2, (bouquet.transform.position.y + curPos.y) / 3, 0); //������� ����� ������ � ������
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
                    snappedPos = new Vector3((basket.transform.position.x + curPos.x) / 2, (basket.transform.position.y + curPos.y) / 5, 0); //������� ����� ������ � �������
                    flowerToDrag.transform.position = Vector3.MoveTowards(flowerToDrag.transform.position, snappedPos, 5); //���������� ������ � �������
                }
                else
                {
                    flowerToDrag.GetComponent<SpriteRenderer>().sortingLayerName = "Default"; //���������� ������ �� ����������� ���� ��� ��������� �� ������
                    flowerToDrag.transform.position = Vector3.MoveTowards(flowerToDrag.transform.position, curPos, 5f); //���������� ������ � �������
                    flowerToDrag.GetComponent<SpriteRenderer>().sprite = flower.imageC;
                }
            }
            else
            {
                flowerToDrag.transform.position = Vector3.MoveTowards(flowerToDrag.transform.position, transform.position, 0.5f); //���������� ������ � ��� ��������
            }

            if (flowerToDrag.transform.position == transform.position) //����� ������ ������� �� ��������
            {
                if (bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() > 0 || basket.GetComponent<ArrangeBasket>().GetFlowerCount() > 0)
                {
                    buttonCreate.interactable = true; //����� ������� ����� ���� ������ ������������ �� �����, � � ���������� ���� �����
                }
                canTake = true;
                Destroy(flowerToDrag);
            }
        }
    }

    private void ReleaseFlower() //������� ������ ������������ � ������
    {
        flowerToDrag = null;
        canTake = true;
        buttonBack.interactable = true;
    }

    public void AllowTaking()
    {
        canTake = true;
    }
}