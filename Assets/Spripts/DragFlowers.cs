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
    public Button buttonCreate;
    public Button buttonBack;

    private GameObject flowerToDrag;
    private bool isDragging = false; //���� �� � ������ � ������� ������
    private bool canTake = true; //����� �� ����� ������

    private void OnMouseDown()
    {
        if (canTake)
        {
            buttonCreate.interactable = false; //��������� ��������� ����� ���� ����� ������
            isDragging = true;
            canTake = false;
            Vector3 spawnPos = new Vector3(mainCam.ScreenToWorldPoint(Input.mousePosition).x, mainCam.ScreenToWorldPoint(Input.mousePosition).y, 0); //���������� �������, �� z=0
            flowerToDrag = Instantiate(flowerPrefab, spawnPos, Quaternion.identity); //����� ������ � ������� ������� � ������ ��� � ����������
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;

        if (bouquet.GetComponent<ArrangeFlowers>().CheckFlowerPos()) //�������� �� ���������� ������ � ������� ������
        {
            flowerToDrag.GetComponent<SpriteRenderer>().sortingLayerName = flower.size;
            ReleaseFlower();
        }
    }

    private void FixedUpdate()
    {
        if (flowerToDrag)
        {
            if (isDragging) //����������� ����� ����� ����� ������
            {
                Vector3 curPos = new Vector3(mainCam.ScreenToWorldPoint(Input.mousePosition).x, mainCam.ScreenToWorldPoint(Input.mousePosition).y, 0); //������� ������� �������

                if (bouquet.GetComponent<ArrangeFlowers>().CheckFlowerInside())
                {
                    Vector3 snappedPos = new Vector3((bouquet.transform.position.x + curPos.x) / 2, (bouquet.transform.position.y + curPos.y) / 3, 0); //������� ����� ������ � ������
                    flowerToDrag.transform.position = Vector3.MoveTowards(flowerToDrag.transform.position, snappedPos, 10); //���������� ������ � �����
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
                else
                {
                    flowerToDrag.transform.position = Vector3.MoveTowards(flowerToDrag.transform.position, curPos, 0.5f); //���������� ������ � �������
                    flowerToDrag.GetComponent<SpriteRenderer>().sprite = flower.imageC;
                }
            }
            else
            {
                flowerToDrag.transform.position = Vector3.MoveTowards(flowerToDrag.transform.position, transform.position, 0.5f); //���������� ������ � ��� ��������
            }

            if (flowerToDrag.transform.position == transform.position) //����� ������ ������� �� ��������
            {
                if (bouquet.GetComponent<ArrangeFlowers>().GetFlowerCount() > 0)
                {
                    buttonCreate.interactable = true; //����� ������� ����� ���� ������ ������������ �� �����, � � ������ ���� �����
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